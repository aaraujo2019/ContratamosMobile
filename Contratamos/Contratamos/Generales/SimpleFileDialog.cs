using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;

namespace Contratamos.Generales
{
    public class SimpleFileDialog
    {
        private AutoResetEvent _autoResetEvent;
        private static int _fileOpen = 0;
        private static int _fileSave = 1;
        private static int _folderChoose = 2;
        private int _selectType = _fileOpen;
        private String _mSdcardDirectory = "";
        private Context _mContext;
        private TextView _mTitleView1;
        private TextView _mTitleView;
        public String DefaultFileName = "*.pdf";
        private String _selectedFileName = "*.pdf";
        private EditText _inputText;

        private String _mDir = "";
        private List<String> _mSubdirs = null;
        private ArrayAdapter<String> _mListAdapter = null;
        private bool _mGoToUpper = false;
        private AlertDialog _dirsDialog;

        public SimpleFileDialog(Context context, FileSelectionMode mode)
        {
            switch (mode)
            {
                case FileSelectionMode.FileOpen:
                    _selectType = _fileOpen;
                    break;
                case FileSelectionMode.FileSave:
                    _selectType = _fileSave;
                    break;
                case FileSelectionMode.FolderChoose:
                    _selectType = _folderChoose;
                    break;
                case FileSelectionMode.FileOpenRoot:
                    _selectType = _fileOpen;
                    _mGoToUpper = true;
                    break;
                case FileSelectionMode.FileSaveRoot:
                    _selectType = _fileSave;
                    _mGoToUpper = true;
                    break;
                case FileSelectionMode.FolderChooseRoot:
                    _selectType = _folderChoose;
                    _mGoToUpper = true;
                    break;
                default:
                    _selectType = _fileOpen;
                    break;
            }


            _mContext = context;
            _mSdcardDirectory = Environment.ExternalStorageDirectory.AbsolutePath;

            try
            {
                _mSdcardDirectory = new File(_mSdcardDirectory).CanonicalPath;
            }
            catch (IOException)
            {
            }
        }

        public enum FileSelectionMode
        {
            FileOpen,
            FileSave,
            FolderChoose,
            FileOpenRoot,
            FileSaveRoot,
            FolderChooseRoot
        }


        public async Task<string> GetFileOrDirectoryAsync(String dir)
        {
            File dirFile = new File(dir);
            while (!dirFile.Exists() || !dirFile.IsDirectory)
            {
                dir = dirFile.Parent;
                dirFile = new File(dir);
            }

            try
            {
                dir = new File(dir).CanonicalPath;
            }
            catch (IOException ioe)
            {
                return _result;
            }

            _mDir = dir;
            _mSubdirs = GetDirectories(dir);

            AlertDialog.Builder dialogBuilder = CreateDirectoryChooserDialog(dir, _mSubdirs, (sender, args) =>
            {
                String mDirOld = _mDir;
                String sel = "" + ((AlertDialog)sender).ListView.Adapter.GetItem(args.Which);
                if (sel[sel.Length - 1] == '/') sel = sel.Substring(0, sel.Length - 1);

                if (sel.Equals("Atras..."))
                {
                    _mDir = _mDir.Substring(0, _mDir.LastIndexOf("/"));
                    if ("".Equals(_mDir))
                    {
                        _mDir = "/";
                    }
                }
                else
                {
                    _mDir += "/" + sel;
                }
                _selectedFileName = DefaultFileName;

                if ((new File(_mDir).IsFile))
                {
                    _mDir = mDirOld;
                    _selectedFileName = sel;
                }

                UpdateDirectory();
            });
            dialogBuilder.SetPositiveButton("OK", (sender, args) =>
            {
                {
                    if (_selectType == _fileOpen || _selectType == _fileSave)
                    {
                        _selectedFileName = _inputText.Text + "";
                        _result = _mDir + "/" + _selectedFileName;
                        _autoResetEvent.Set();
                    }
                    else
                    {
                        _result = _mDir;
                        _autoResetEvent.Set();
                    }
                }

            });
            dialogBuilder.SetNegativeButton("Cancelar", (sender, args) => { });
            _dirsDialog = dialogBuilder.Create();

            _dirsDialog.CancelEvent += (sender, args) => { _autoResetEvent.Set(); };
            _dirsDialog.DismissEvent += (sender, args) => { _autoResetEvent.Set(); };

            _autoResetEvent = new AutoResetEvent(false);
            _dirsDialog.Show();

            await Task.Run(() => { _autoResetEvent.WaitOne(); });

            return _result;
        }

        private string _result = null;

        private bool CreateSubDir(String newDir)
        {
            File newDirFile = new File(newDir);
            if (!newDirFile.Exists()) return newDirFile.Mkdir();
            else return false;
        }

        private List<String> GetDirectories(String dir)
        {
            List<String> dirs = new List<String>();
            try
            {
                File dirFile = new File(dir);

                // if directory is not the base sd card directory add ".." for going up one directory
                if ((_mGoToUpper || !_mDir.Equals(_mSdcardDirectory)) && !"/".Equals(_mDir))
                    dirs.Add("Atras...");

                if (!dirFile.Exists() || !dirFile.IsDirectory)
                {
                    return dirs;
                }

                foreach (File file in dirFile.ListFiles())
                {
                    if (file.IsDirectory)
                        dirs.Add(file.Name + "/");
                    else if (_selectType == _fileSave || _selectType == _fileOpen)
                        dirs.Add(file.Name);
                }
            }
            catch (Exception e) { }

            dirs.Sort();
            return dirs;
        }

        private AlertDialog.Builder CreateDirectoryChooserDialog(String title, List<String> listItems, EventHandler<DialogClickEventArgs> onClickListener)
        {
            AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(_mContext);

            _mTitleView1 = new TextView(_mContext);
            _mTitleView1.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

            if (_selectType == _fileOpen) _mTitleView1.Text = "Abrir";
            if (_selectType == _fileSave) _mTitleView1.Text = "Guardar como";
            if (_selectType == _folderChoose) _mTitleView1.Text = "Selecionar carpeta...";

            _mTitleView1.Gravity = GravityFlags.CenterVertical;
            _mTitleView1.SetTextColor(Color.Black);
            _mTitleView1.SetTextSize(ComplexUnitType.Dip, 18);
            _mTitleView1.SetTypeface(null, TypefaceStyle.Bold);

            LinearLayout titleLayout1 = new LinearLayout(_mContext);
            titleLayout1.Orientation = Orientation.Vertical;
            titleLayout1.AddView(_mTitleView1);

            if (_selectType == _fileSave)
            {
                Button newDirButton = new Button(_mContext);
                newDirButton.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                newDirButton.Text = "Nueva Carpeta.";
                newDirButton.Click += (sender, args) =>
                {
                    EditText input = new EditText(_mContext);
                    new AlertDialog.Builder(_mContext).SetTitle("Nombre del Carpeta.").SetView(input).SetPositiveButton("OK", (o, eventArgs) =>
                    {
                        String newDirName = input.Text;
                        if (CreateSubDir(_mDir + "/" + newDirName))
                        {
                            _mDir += "/" + newDirName;
                            UpdateDirectory();
                        }
                        else
                        {
                            Toast.MakeText(_mContext, "Failed to create '" + newDirName + "' folder", ToastLength.Short).Show();
                        }
                    }).SetNegativeButton("Cancel", (o, eventArgs) => { }).Show();
                };
                titleLayout1.AddView(newDirButton);
            }

            LinearLayout titleLayout = new LinearLayout(_mContext);
            titleLayout.Orientation = Orientation.Vertical;

            var currentSelection = new TextView(_mContext);
            currentSelection.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            currentSelection.SetTextColor(Color.Black);
            currentSelection.Gravity = GravityFlags.CenterVertical;
            currentSelection.Text = "Current selection:";
            currentSelection.SetTextSize(ComplexUnitType.Dip, 12);
            currentSelection.SetTypeface(null, TypefaceStyle.Bold);

            titleLayout.AddView(currentSelection);

            _mTitleView = new TextView(_mContext);
            _mTitleView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            _mTitleView.SetTextColor(Color.Black);
            _mTitleView.Gravity = GravityFlags.CenterVertical;
            _mTitleView.Text = title;
            _mTitleView.SetTextSize(ComplexUnitType.Dip, 10);
            _mTitleView.SetTypeface(null, TypefaceStyle.Normal);

            titleLayout.AddView(_mTitleView);

            if (_selectType == _fileOpen || _selectType == _fileSave)
            {
                _inputText = new EditText(_mContext);
                _inputText.Text = DefaultFileName;
                titleLayout.AddView(_inputText);
            }

            dialogBuilder.SetView(titleLayout);
            dialogBuilder.SetCustomTitle(titleLayout1);
            _mListAdapter = CreateListAdapter(listItems);
            dialogBuilder.SetSingleChoiceItems(_mListAdapter, -1, onClickListener);
            dialogBuilder.SetCancelable(true);
            return dialogBuilder;
        }


        private void UpdateDirectory()
        {
            _mSubdirs.Clear();
            _mSubdirs.AddRange(GetDirectories(_mDir));
            _mTitleView.Text = _mDir;
            _dirsDialog.ListView.Adapter = null;
            _dirsDialog.ListView.Adapter = CreateListAdapter(_mSubdirs);

            if (_selectType == _fileSave || _selectType == _fileOpen)
            {
                _inputText.Text = _selectedFileName;
            }
        }

        private ArrayAdapter<String> CreateListAdapter(List<String> items)
        {
            var adapter = new SimpleArrayAdaper(_mContext, Android.Resource.Layout.SelectDialogItem, Android.Resource.Id.Text1, items);
            return adapter;
        }
    }

    internal class SimpleArrayAdaper : ArrayAdapter<String>
    {
        public SimpleArrayAdaper(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public SimpleArrayAdaper(Context context, int textViewResourceId) : base(context, textViewResourceId)
        {
        }

        public SimpleArrayAdaper(Context context, int resource, int textViewResourceId) : base(context, resource, textViewResourceId)
        {
        }

        public SimpleArrayAdaper(Context context, int textViewResourceId, string[] objects) : base(context, textViewResourceId, objects)
        {
        }

        public SimpleArrayAdaper(Context context, int resource, int textViewResourceId, string[] objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public SimpleArrayAdaper(Context context, int textViewResourceId, IList<string> objects) : base(context, textViewResourceId, objects)
        {
        }

        public SimpleArrayAdaper(Context context, int resource, int textViewResourceId, IList<string> objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = base.GetView(position, convertView, parent);
            if (v is TextView)
            {
                TextView tv = (TextView)v;
                tv.LayoutParameters.Height = ViewGroup.LayoutParams.WrapContent;
                tv.Ellipsize = null;
            }
            return v;
        }
    }
}
