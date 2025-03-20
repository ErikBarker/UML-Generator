using System.Windows;

namespace UMLGenerator;
public partial class RuleWindowCreationPopup : Window
{
    public String LanguageName;
    public String Version;

    public bool save = false;

    public RuleWindowCreationPopup(){

        InitializeComponent();

    }

    public void CancelButton_Click(object sender, RoutedEventArgs e){
        save = false;
        Close();
    }

    public void SaveButton_Click(object sender, RoutedEventArgs e){
        save = true;

        if (!LanguageTextBox.Text.Equals("") && !VertionTextBox.Text.Equals(""))
        {
            LanguageName = LanguageTextBox.Text;
            Version = VertionTextBox.Text;

            Close();

            return;
        }

        MessageBox.Show("Please Fill out all fields");
    }


}