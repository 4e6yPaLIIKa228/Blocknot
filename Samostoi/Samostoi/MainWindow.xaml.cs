using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;



namespace Samostoi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) //открытие документа
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(RichTxBox.Document.ContentStart, RichTxBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (System.IO.Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (System.IO.Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                    else
                        doc.Load(fs, DataFormats.Xaml);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)//сохранение файла
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(RichTxBox.Document.ContentStart, RichTxBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                }
            }
            
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) //Новый документ
        {
            if (new TextRange(RichTxBox.Document.ContentStart, RichTxBox.Document.ContentEnd).Text.Length > 1) 
            {
                MessageBoxResult result = MessageBox.Show("Сохранить документ?", "Предупрежение", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                     SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                   if (sfd.ShowDialog() == true)
                   {
                            TextRange doc = new TextRange(RichTxBox.Document.ContentStart, RichTxBox.Document.ContentEnd);
                            using (FileStream fs = File.Create(sfd.FileName))
                            {
                                if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                                    doc.Save(fs, DataFormats.Rtf);
                                else if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                                    doc.Save(fs, DataFormats.Text);
                                else
                                    doc.Save(fs, DataFormats.Xaml);
                            }
                   }
                                    break;
                        case MessageBoxResult.No:
                        RichTxBox.Document.Blocks.Clear();
                        break;
                }

            } 

        }
    }
}
