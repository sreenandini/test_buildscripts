using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using BMC.CoreLib.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace BMC.CoreLib.Win32
{
    public class HomeScreenWidgets : ControlBase
    {
        private IsolatedStorageFile _storage = null;
        private IDictionary<string, int> _widgetsDict = null;
        private IList<string> _widgets = null;
        private WidgetDetailsHash _widgetsSchema = null;

        private int _widgetHeight = 155;
        private int _widgetWidth = 230;
        private FlowLayoutPanel _container = null;
        protected SolidBrush _backgroundBrush = null;

        private const string SCHEMA_FILE = "WIDGETDETAILS.XML";

        public event EventHandler<WidgetEventArgs> WidgetLoad = null;
        public event EventHandler<WidgetEventArgs> WidgetClick = null;

        [Serializable]
        public class WidgetDetails
        {
            public string FileName { get; set; }
            public string UniqueKey { get; set; }
            public string Description { get; set; }
        }

        [Serializable]
        public class WidgetDetailsHash : Dictionary<string, WidgetDetails>
        {
            public WidgetDetailsHash()
                : base(StringComparer.InvariantCultureIgnoreCase) { }
        }

        public HomeScreenWidgets()
        {
            //base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
            _storage = IsolatedStorageFile.GetUserStoreForAssembly();
            _widgetsDict = new SortedDictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            _widgetsSchema = null;// new WidgetDetailsHash();
            _widgets = new List<string>();
            this.AddContainer();
        }

        private void AddContainer()
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "AddContainer");

            try
            {
                _container = new FlowLayoutPanel();
                _container.Dock = DockStyle.Fill;
                _container.AutoScroll = true;
                this.Controls.Clear();
                this.Controls.Add(_container);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (_backgroundBrush == null ||
                    _backgroundBrush.Color != value)
                {
                    if (_backgroundBrush != null)
                    {
                        _backgroundBrush.Dispose();
                    }
                    _backgroundBrush = new SolidBrush(value);
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Dimension")]
        public int WidgetHeight
        {
            get { return _widgetHeight; }
            set { _widgetHeight = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Dimension")]
        public int WidgetWidth
        {
            get { return _widgetWidth; }
            set { _widgetWidth = value; }
        }

        private Color _borderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(160)))), ((int)(((byte)(166)))));

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        private Color _borderColorHover = Color.FromArgb(255, 156, 161, 167);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Color BorderColorHover
        {
            get { return _borderColorHover; }
            set
            {
                _borderColorHover = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Files")]
        public string Directory { get; set; }

        private string EnsureDirectory()
        {
            if (this.Directory.IsEmpty()) return string.Empty;
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "EnsureDirectory");
            string result = string.Empty;

            try
            {
                string[] dirNames = _storage.GetDirectoryNames(this.Directory);
                if (dirNames == null || dirNames.Length == 0)
                {
                    _storage.CreateDirectory(this.Directory);
                }
                result = this.Directory;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private string GetFilePattern(string pattern)
        {
            string directory = this.EnsureDirectory();
            if (directory.IsEmpty())
                return pattern;
            else
                return directory + "\\" + pattern;
        }

        public void LoadWidgets()
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "LoadItems");

            try
            {
                this.EnsureDirectory();
                _widgets.Clear();
                _widgetsDict.Clear();
                this.LoadSchemaDetails();

                string[] fileNames = (from s in _storage.GetFileNames(this.GetFilePattern("*.png"))
                                      orderby s
                                      select s).ToArray();
                if (fileNames != null)
                {
                    foreach (string fileName in fileNames)
                    {
                        if (!_widgetsDict.ContainsKey(fileName))
                        {
                            int index = _widgets.Count;
                            _widgets.Add(fileName);
                            _widgetsDict.Add(fileName, index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.DrawWidgets();
            }
        }

        private void PerformSchemaWork(FileAccess access, Action<Stream> doWork)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "PerformSchemaWork");
            Stream st = null;

            try
            {
                // load the schema details
                string fileName = this.GetFilePattern(SCHEMA_FILE);
                string[] schemaFile = _storage.GetFileNames(fileName);
                if (schemaFile == null || schemaFile.Length == 0)
                {
                    access = FileAccess.Write;
                    st = new IsolatedStorageFileStream(fileName, FileMode.Create, access, _storage);
                }
                else
                {
                    st = new IsolatedStorageFileStream(fileName, FileMode.Open, access, _storage);
                }
                doWork(st);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (st != null)
                {
                    st.Dispose();
                }
            }
        }

        private void LoadSchemaDetails()
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "LoadSchemaDetails");

            try
            {
                this.PerformSchemaWork(FileAccess.Read, (s) =>
                {
                    try
                    {
                        if (_widgetsSchema != null)
                        {
                            _widgetsSchema.Clear();
                            _widgetsSchema = null;
                        }
                        _widgetsSchema = Extensions.ReadDataContractObject<WidgetDetailsHash>(s) as WidgetDetailsHash;
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (_widgetsSchema == null)
                {
                    _widgetsSchema = new WidgetDetailsHash();
                }
            }
        }

        private void SaveSchemaDetails()
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "LoadSchemaDetails");

            this.PerformSchemaWork(FileAccess.Write, (s) =>
            {
                try
                {
                    Extensions.WriteDataContractObject<WidgetDetailsHash>(_widgetsSchema, s);
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                finally
                {
                    if (_widgetsSchema == null)
                    {
                        _widgetsSchema = new WidgetDetailsHash();
                    }
                }
            });
        }

        private void DrawWidgets()
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "DrawWidgets");

            try
            {
                this.SuspendLayout();
                _container.Controls.Clear();
                foreach (string fileName in _widgets)
                {
                    this.LoadImage(fileName);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.ResumeLayout(true);
            }
        }

        private void LoadImage(string fileName)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "LoadImage");
            WidgetDetails widgetDetails = null;

            try
            {
                string actualFileName = this.GetFilePattern(fileName);
                using (Stream st = new IsolatedStorageFileStream(actualFileName, FileMode.Open, FileAccess.Read, _storage))
                {
                    string text = Path.GetFileNameWithoutExtension(fileName);
                    if (_widgetsSchema.ContainsKey(fileName))
                    {
                        widgetDetails = _widgetsSchema[fileName];
                        text = widgetDetails.Description;

                        PreviewWidget wid = new PreviewWidget(this.WidgetWidth, this.WidgetHeight, new Bitmap(st), false);
                        wid.Text = text;
                        wid.BorderColor = this.BorderColor;
                        wid.BorderColorHover = this.BorderColorHover;
                        wid.ForeColor = Color.Black;
                        wid.Margin = new Padding(3);
                        wid.Tag = widgetDetails;
                        wid.Cursor = Cursors.Hand;
                        wid.Click += new EventHandler(OnWidget_Click);
                        wid.CloseClick += new EventHandler(OnWidget_CloseClick);
                        _container.Controls.Add(wid);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (widgetDetails != null)
                {
                    this.OnWidgetLoad(_widgetsSchema[fileName]);
                }
            }
        }

        private void OnWidgetLoad(WidgetDetails widget)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidgetLoad");

            try
            {
                if (this.WidgetLoad != null)
                {
                    this.WidgetLoad(this, new WidgetEventArgs(widget));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void OnWidgetClick(WidgetDetails widget)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidgetLoad");

            try
            {
                if (this.WidgetClick != null)
                {
                    this.WidgetClick(this, new WidgetEventArgs(widget));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnWidget_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidget_Click");

            try
            {
                PreviewWidget widget = sender as PreviewWidget;
                WidgetDetails widgetDetails = widget.Tag as WidgetDetails;
                this.OnWidgetClick(widgetDetails);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnWidget_CloseClick(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidget_Click");

            try
            {
                PreviewWidget widget = sender as PreviewWidget;
                WidgetDetails widgetDetails = widget.Tag as WidgetDetails;
                widget.CloseClick -= this.OnWidget_CloseClick;
                _storage.DeleteFile(this.GetFilePattern(widgetDetails.FileName));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.LoadWidgets();
            }
        }

        private IsolatedStorageFileStream GetStream(string fileName, FileMode mode, FileAccess fileAccess)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "GetStream");
            IsolatedStorageFileStream result = default(IsolatedStorageFileStream);

            try
            {
                if (_widgetsDict.ContainsKey(fileName))
                {
                    result = new IsolatedStorageFileStream(fileName, mode, fileAccess, _storage);
                }
                else
                {
                    result = new IsolatedStorageFileStream(fileName, FileMode.Create, fileAccess, _storage);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public WidgetDetails CaptureWidget(object owner, string uniqueKey, string description, Rectangle bounds)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "LoadItems");
            WidgetDetails details = null;

            try
            {
                Form ownerForm = owner as Form;
                string getTypeHash = owner.GetType().ToString().ToLower().GetHashCode().ToString();
                string uniquekeyHash = uniqueKey.ToLower().GetHashCode().ToString();
                string fileName = "F_" + getTypeHash + "_" + uniquekeyHash + ".png";
                string actualFileName = this.GetFilePattern(fileName);

                using (IsolatedStorageFileStream stream = this.GetStream(actualFileName, FileMode.Create, FileAccess.Write))
                {
                    if (_widgetsSchema.ContainsKey(fileName))
                    {
                        details = _widgetsSchema[fileName];
                    }
                    else
                    {
                        details = new WidgetDetails()
                         {
                             FileName = fileName,
                             UniqueKey = uniqueKey
                         };
                        _widgetsSchema.Add(fileName, details);
                    }
                    details.Description = !description.IsEmpty() ? description : Path.GetFileName(fileName);

                    this.SaveSchemaDetails();
                    if (ownerForm != null)
                    {
                        Extensions.SaveScreenshotToStream(ownerForm, stream);
                    }
                    else
                    {
                        Extensions.SaveScreenshotToStream(bounds, stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.LoadWidgets();
            }

            return details;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.BackColor != Color.Transparent)
            {
                pevent.Graphics.FillRectangle(_backgroundBrush, this.ClientRectangle);

                if (this.BackgroundImage != null &&
                    this.BackgroundImageLayout == ImageLayout.Tile)
                {
                    using (TextureBrush textureBrush = new TextureBrush(this.BackgroundImage, WrapMode.Tile))
                    {
                        pevent.Graphics.FillRectangle(textureBrush, this.ClientRectangle);
                    }
                    return;
                }
            }
            base.OnPaintBackground(pevent);
        }
    }

    public class WidgetEventArgs : EventArgs
    {
        public WidgetEventArgs(HomeScreenWidgets.WidgetDetails widget)
        {
            this.Widget = widget;
        }

        public HomeScreenWidgets.WidgetDetails Widget { get; private set; }
    }
}
