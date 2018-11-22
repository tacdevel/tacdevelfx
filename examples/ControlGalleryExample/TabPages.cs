using TCD.UI.Controls;
using TCD.UI.Controls.Containers;

namespace ControlGalleryExample
{
    public sealed class BasicControlsTab : TabPage
    {
        private static VerticalContainer vContainer = new VerticalContainer() { IsPadded = true };
        private HorizontalContainer hContainer = new HorizontalContainer() { IsPadded = true };
        private Button button = new Button("Button");
        private CheckBox checkBox = new CheckBox("CheckBox");
        private Label label = new Label("This is a Label. Right now, labels can only span one line.");
        private HorizontalSeparator hSeparator = new HorizontalSeparator();
        private GroupContainer groupContainer = new GroupContainer("Entries") { IsMargined = true };
        private FormContainer formContainer = new FormContainer { IsPadded = true };
        private TextBox textBox = new TextBox();
        private PasswordBox passwordBox = new PasswordBox();
        private SearchBox searchBox = new SearchBox();
        private TextBlock textBlock = new TextBlock();
        private NonWrappingTextBlock noWordWrapTextBlock = new NonWrappingTextBlock();

        public BasicControlsTab() : base("Basic Controls", vContainer) => InitializeComponent();

        protected override void InitializeComponent()
        {
            IsMargined = true;

            vContainer.Children.Add(hContainer);
            hContainer.Children.Add(button);
            hContainer.Children.Add(checkBox);
            vContainer.Children.Add(label);
            vContainer.Children.Add(hSeparator);
            vContainer.Children.Add(groupContainer, true);
            groupContainer.Child = formContainer;
            formContainer.Children.Add("TextBox", textBox);
            formContainer.Children.Add("PasswordBox", passwordBox);
            formContainer.Children.Add("SearchBox", searchBox);
            formContainer.Children.Add("Multiline TextBox", textBlock, true);
            formContainer.Children.Add("Multiline TextBox No WordWrap", noWordWrapTextBlock, true);
        }
    }

    public sealed class NumbersTab : TabPage
    {
        private static HorizontalContainer hContainer = new HorizontalContainer() { IsPadded = true };
        private GroupContainer groupContainer = new GroupContainer("Numbers") { IsMargined = true };
        private static VerticalContainer vContainer = new VerticalContainer() { IsPadded = true };
        private SpinBox spinBox = new SpinBox(0, 100);
        private Slider slider = new Slider(0, 100);
        private ProgressBar progressBar = new ProgressBar();
        private ProgressBar iProgressBar = new ProgressBar() { Value = -1 };
        private GroupContainer groupContainer2 = new GroupContainer("Lists") { IsMargined = true };
        private static VerticalContainer vContainer2 = new VerticalContainer() { IsPadded = true };
        private ComboBox comboBox = new ComboBox();
        private EditableComboBox editableComboBox = new EditableComboBox();
        private RadioButtonList radioButtonList = new RadioButtonList();

        public NumbersTab() : base("Numbers and Lists", hContainer) => InitializeComponent();

        protected override void InitializeComponent()
        {
            IsMargined = true;

            hContainer.Children.Add(groupContainer, true);
            groupContainer.Child = vContainer;

            spinBox.ValueChanged += (sender) => 
            {
                int value = spinBox.Value;
                slider.Value = value;
                progressBar.Value = value;
            };

            slider.ValueChanged += (sender) =>
            {
                int value = slider.Value;
                spinBox.Value = value;
                progressBar.Value = value;
            };

            vContainer.Children.Add(spinBox);
            vContainer.Children.Add(slider);
            vContainer.Children.Add(progressBar);
            vContainer.Children.Add(iProgressBar);

            hContainer.Children.Add(groupContainer2, true);

            groupContainer2.Child = vContainer2;

            comboBox.Add("Combobox Item 1", "Combobox Item 2", "Combobox Item 3");
            editableComboBox.Add("Editable Item 1", "Editable Item 2", "Editable Item 3");
            radioButtonList.Add("Radio Button 1", "Radio Button 2", "Radio Button 3");

            vContainer2.Children.Add(comboBox);
            vContainer2.Children.Add(editableComboBox);
            vContainer2.Children.Add(radioButtonList);
        }
    }

    public sealed class DataChoosersTab : TabPage
    {
        private StackContainer hPanel = new StackContainer(Orientation.Horizontal) { IsPadded = true };
        private StackContainer vPanel = new StackContainer(Orientation.Vertical) { IsPadded = true };
        private DatePicker datePicker = new DatePicker();
        private TimePicker timePicker = new TimePicker();
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private FontPicker fontPicker = new FontPicker();
        private ColorPicker colorPicker = new ColorPicker();
        private Separator hSeparator = new Separator(Orientation.Horizontal);

        private StackContainer vPanel2 = new StackContainer(Orientation.Vertical) { IsPadded = true };
        private GridContainer gridFile = new GridContainer() { IsPadded = true };
        private Button buttonOpenFile = new Button("Open File");
        private TextBox textboxOpenFile = new TextBox() { IsReadOnly = true };
        private Button buttonSaveFile = new Button("Save File");
        private TextBox textboxSaveFile = new TextBox() { IsReadOnly = true };

        private StackContainer hPanelMessages = new StackContainer(Orientation.Horizontal) { IsPadded = true };
        private Button buttonMessage = new Button("Message Box");
        private Button buttonMessageErr = new Button("Message Box (Error)");

        public DataChoosersTab() : base("Data Choosers") => InitializeComponent();

        protected override void InitializeComponent()
        {
            IsMargined = true;
            Child = hPanel;

            hPanel.Items.Add(vPanel);

            vPanel.Items.Add(datePicker);
            vPanel.Items.Add(timePicker);
            vPanel.Items.Add(dateTimePicker);
            vPanel.Items.Add(fontPicker);
            vPanel.Items.Add(colorPicker);

            hPanel.Items.Add(hSeparator);
            hPanel.Items.Add(vPanel2);

            vPanel2.Items.Add(gridFile);

            buttonOpenFile.Click += (sender, args) =>
            {
                if (Window.ShowOpenFileDialog(out string path, null))
                    textboxOpenFile.Text = path;
                else
                    textboxOpenFile.Text = "(null)";
            };

            buttonSaveFile.Click += (sender, args) =>
            {
                if (Window.ShowSaveFileDialog(out string path, null))
                    textboxSaveFile.Text = path;
                else
                    textboxSaveFile.Text = "(null)";
            };

            buttonMessage.Click += (sender, args) => { Window.ShowMessageBox(null, "This is a normal message box.", "More detailed information can be shown here."); };
            buttonMessageErr.Click += (sender, args) => { Window.ShowMessageBox(null, "This message box describes an error.", "More detailed information can be shown here.", true); };

            gridFile.Items.Add(buttonOpenFile, 0, 0, 1, 1, 0, 0, Alignment.Fill);
            gridFile.Items.Add(textboxOpenFile, 1, 0, 1, 1, 1, 0, Alignment.Fill);
            gridFile.Items.Add(buttonSaveFile, 0, 1, 1, 1, 0, 0, Alignment.Fill);
            gridFile.Items.Add(textboxSaveFile, 1, 1, 1, 1, 1, 0, Alignment.Fill);

            hPanelMessages.Items.Add(buttonMessage);
            hPanelMessages.Items.Add(buttonMessageErr);

            vPanel2.Items.Add(hPanelMessages);
        }
    }
}