using PolishAngielski.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PolishAngielski
{
    public partial class MainWindow : Window
    {
        int minvalue = 1, maxvalue = 100, startvalue = 10;
        public ObservableCollection<Word> ListOfWords { get; set; }
        LearnTest learnTest;
        OneTest oneTest;
        ManyTest manyTest;
        WriteTest writeTest;
        int questionIndex;
        public MainWindow()
        {
            InitializeComponent();

            Program.words = WordBase.getInstance();
            Program.adjectives = AdjectiveBase.getInstance();
            Program.LoadFromFile();
            ListOfWords = Program.words.wordList;

            wordList_lv.ItemsSource = ListOfWords;
            NUDTextBox_LearnTest.Text = startvalue.ToString();
            NUDTextBox_OneTest.Text = startvalue.ToString();
            NUDTextBox_ManyTest.Text = startvalue.ToString();
            NUDTextBox_WriteTest.Text = startvalue.ToString();
            questionIndex = 0;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 0;
            questionIndex = 0;

            learnTestMode_Page1.Visibility = Visibility.Visible;
            learnTestMode_Page2.Visibility = Visibility.Collapsed;
            learnTestMode_Page3.Visibility = Visibility.Collapsed;

            oneTestMode_Page1.Visibility = Visibility.Visible;
            oneTestMode_Page2.Visibility = Visibility.Collapsed;
            oneTestMode_Page3.Visibility = Visibility.Collapsed;

            manyTestMode_Page1.Visibility = Visibility.Visible;
            manyTestMode_Page2.Visibility = Visibility.Collapsed;
            manyTestMode_Page3.Visibility = Visibility.Collapsed;

            writeTestMode_Page1.Visibility = Visibility.Visible;
            writeTestMode_Page2.Visibility = Visibility.Collapsed;
            writeTestMode_Page3.Visibility = Visibility.Collapsed;
        }
        private void LearnMode_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 1;
        }
        private void OneTestMode_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 2;
        }
        private void MultipleTestMode_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 3;
        }
        private void WriteTestMode_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 4;
        }
        private void BaseMode_Click(object sender, RoutedEventArgs e)
        {
            menu_tc.SelectedIndex = 5;
        }
        private void NewWord_Click(object sender, RoutedEventArgs e)
        {
            Program.words.wordList.Add(new Word());
        }
        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            if (wordList_lv.SelectedItem != null)
                Program.words.wordList.Remove((Word)(wordList_lv.Items.GetItemAt(wordList_lv.SelectedIndex)));
            else
                MessageBox.Show("Musisz zaznaczyć słowo!");
        }
        //-----------------------------end----------------------------


        //-----------------------------test nauki----------------------------
        private int learnTestQuestionNumber;
        //obsługa numberpickera
        private void NUDButtonUP_LearnTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_LearnTest.Text != "") number = Convert.ToInt32(NUDTextBox_LearnTest.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox_LearnTest.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_LearnTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_LearnTest.Text != "") number = Convert.ToInt32(NUDTextBox_LearnTest.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox_LearnTest.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_LearnTest_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP_LearnTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_LearnTest, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown_LearnTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_LearnTest, new object[] { true });
            }
        }

        private void NUDTextBox_LearnTest_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_LearnTest, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_LearnTest, new object[] { false });
        }

        private void NUDTextBox_LearnTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox_LearnTest.Text != "")
                if (!int.TryParse(NUDTextBox_LearnTest.Text, out number)) NUDTextBox_LearnTest.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox_LearnTest.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox_LearnTest.Text = minvalue.ToString();
            NUDTextBox_LearnTest.SelectionStart = NUDTextBox_LearnTest.Text.Length;

        }

        private void learnTestMode_Page1_Start_Click(object sender, RoutedEventArgs e)
        {
            LearnTestBuilder learnTestBuilder = new LearnTestBuilder();
            learnTestBuilder.CollectTestInfo(Int32.Parse(NUDTextBox_LearnTest.Text), (bool)addAdjectives_LearnTest_cb.IsChecked, (bool)nativeLanguage_LearnTest_cb.IsChecked);
            learnTestBuilder.PrepareList();
            learnTest = learnTestBuilder.GetTest();

            learnTestShowQuestion();

            learnTestMode_Page1.Visibility = Visibility.Collapsed;
            learnTestMode_Page2.Visibility = Visibility.Visible;

            learnTestQuestionNumber = learnTest.questions.Count;
        }

        private void learnTestShowQuestion()
        {
            learnTestMode_pb.Value = (int)(((double)learnTest.allAnswers / learnTestQuestionNumber) * 100.0);
            learnTestMode_Page2_answer1.IsChecked = false;
            learnTestMode_Page2_answer2.IsChecked = false;
            learnTestMode_Page2_answer3.IsChecked = false;
            learnTestMode_Page2_answer4.IsChecked = false;
            if (learnTest.nativeLanguage)
            {
                learnTestMode_Page2_wordToTranslate_l.Content = learnTest.questions.ElementAt(questionIndex).GetEnglish();
                Answer answers = learnTest.answers.ElementAt(questionIndex);
                learnTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetPolish();
                learnTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetPolish();
                learnTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetPolish();
                learnTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetPolish();
            }
            else
            {
                learnTestMode_Page2_wordToTranslate_l.Content = learnTest.questions.ElementAt(questionIndex).GetPolish();
                Answer answers = learnTest.answers.ElementAt(questionIndex);
                learnTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetEnglish();
                learnTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetEnglish();
                learnTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetEnglish();
                learnTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetEnglish();
            }
        }
        private void learnTestCheckAnswer()
        {
            String answer = "";
            if ((bool)learnTestMode_Page2_answer1.IsChecked)
                answer = "0";
            else if ((bool)learnTestMode_Page2_answer2.IsChecked)
                answer = "1";
            else if ((bool)learnTestMode_Page2_answer3.IsChecked)
                answer = "2";
            else if ((bool)learnTestMode_Page2_answer4.IsChecked)
                answer = "3";
            else
                answer = "-1";
            if (learnTest.CheckAnswer(questionIndex, answer))
                questionIndex--;
            else
            {
                if (learnTest.nativeLanguage)
                    MessageBox.Show("Poprawna odpowiedź to: " + learnTest.questions[questionIndex].GetPolish());
                else
                    MessageBox.Show("Poprawna odpowiedź to: " + learnTest.questions[questionIndex].GetEnglish());
            }
        }
        private void learnTestMode_Page2_Next_Click(object sender, RoutedEventArgs e)
        {
            if (questionIndex < learnTest.questions.Count)
            {
                learnTestCheckAnswer();
                questionIndex++;
                if (questionIndex == learnTest.questions.Count)
                {
                    questionIndex = 0;
                    learnTest.CheckAnswer(questionIndex, "-2");
                }
                if (learnTest.questions.Count != 0)
                    learnTestShowQuestion();
                else
                {
                    questionIndex = 0;
                    learnTestMode_Page3_summary_l.Content = learnTest.ShowScore();
                    learnTestMode_Page2.Visibility = Visibility.Collapsed;
                    learnTestMode_Page3.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (learnTest.questions.Count != 0)
                    learnTestCheckAnswer();
                questionIndex = 0;
                learnTestMode_Page3_summary_l.Content = learnTest.ShowScore();
                learnTestMode_Page2.Visibility = Visibility.Collapsed;
                learnTestMode_Page3.Visibility = Visibility.Visible;
            }
        }
        private void learnTestMode_Page3_End_Click(object sender, RoutedEventArgs e)
        {
            learnTestMode_Page3.Visibility = Visibility.Collapsed;
            learnTestMode_Page1.Visibility = Visibility.Visible;
            menu_tc.SelectedIndex = 0;
        }
        //-----------------------------end----------------------------

        //-----------------------------test jednokrotnego wyboru----------------------------
        //obsługa numberpickera
        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_OneTest.Text != "") number = Convert.ToInt32(NUDTextBox_OneTest.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox_OneTest.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_OneTest.Text != "") number = Convert.ToInt32(NUDTextBox_OneTest.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox_OneTest.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP_OneTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_OneTest, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown_OneTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_OneTest, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_OneTest, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_OneTest, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox_OneTest.Text != "")
                if (!int.TryParse(NUDTextBox_OneTest.Text, out number)) NUDTextBox_OneTest.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox_OneTest.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox_OneTest.Text = minvalue.ToString();
            NUDTextBox_OneTest.SelectionStart = NUDTextBox_OneTest.Text.Length;

        }

        private void oneTestMode_Page1_Start_Click(object sender, RoutedEventArgs e)
        {
            OneTestBuilder oneTestBuilder = new OneTestBuilder();
            oneTestBuilder.CollectTestInfo(Int32.Parse(NUDTextBox_OneTest.Text), (bool)addAdjectives_OneTest_cb.IsChecked, (bool)nativeLanguage_OneTest_cb.IsChecked);
            oneTestBuilder.PrepareList();
            oneTest = oneTestBuilder.GetTest();

            oneTestShowQuestion();

            oneTestMode_Page1.Visibility = Visibility.Collapsed;
            oneTestMode_Page2.Visibility = Visibility.Visible;
        }

        private void oneTestShowQuestion()
        {
            oneTestMode_pb.Value = (int)(((double)questionIndex / oneTest.questions.Count) * 100.0);
            oneTestMode_Page2_answer1.IsChecked = false;
            oneTestMode_Page2_answer2.IsChecked = false;
            oneTestMode_Page2_answer3.IsChecked = false;
            oneTestMode_Page2_answer4.IsChecked = false;
            if (oneTest.nativeLanguage)
            {
                oneTestMode_Page2_wordToTranslate_l.Content = oneTest.questions.ElementAt(questionIndex).GetEnglish();
                Answer answers = oneTest.answers.ElementAt(questionIndex);
                oneTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetPolish();
                oneTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetPolish();
                oneTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetPolish();
                oneTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetPolish();
            }
            else
            {
                oneTestMode_Page2_wordToTranslate_l.Content = oneTest.questions.ElementAt(questionIndex).GetPolish();
                Answer answers = oneTest.answers.ElementAt(questionIndex);
                oneTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetEnglish();
                oneTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetEnglish();
                oneTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetEnglish();
                oneTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetEnglish();
            }
        }
        private void oneTestCheckAnswer()
        {
            bool checked1 = (bool)oneTestMode_Page2_answer1.IsChecked;
            bool checked2 = (bool)oneTestMode_Page2_answer2.IsChecked;
            bool checked3 = (bool)oneTestMode_Page2_answer3.IsChecked;
            bool checked4 = (bool)oneTestMode_Page2_answer4.IsChecked;
            if (checked1)
                oneTest.CheckAnswer(questionIndex - 1, "0");
            else if (checked2)
                oneTest.CheckAnswer(questionIndex - 1, "1");
            else if (checked3)
                oneTest.CheckAnswer(questionIndex - 1, "2");
            else if (checked4)
                oneTest.CheckAnswer(questionIndex - 1, "3");
        }
        private void oneTestMode_Page2_Next_Click(object sender, RoutedEventArgs e)
        {
            questionIndex++;
            if (questionIndex < oneTest.questions.Count)
            {
                oneTestCheckAnswer();
                oneTestShowQuestion();
            }
            else
            {
                oneTestCheckAnswer();
                questionIndex = 0;
                oneTestMode_Page3_summary_l.Content = oneTest.ShowScore();
                oneTestMode_Page2.Visibility = Visibility.Collapsed;
                oneTestMode_Page3.Visibility = Visibility.Visible;
            }
        }
        private void oneTestMode_Page3_End_Click(object sender, RoutedEventArgs e)
        {
            oneTestMode_Page3.Visibility = Visibility.Collapsed;
            oneTestMode_Page1.Visibility = Visibility.Visible;
            menu_tc.SelectedIndex = 0;
        }
        //-----------------------------end----------------------------

        //-----------------------------test wielokrotnego wyboru----------------------------
        //obsługa numberpickera
        private void NUDButtonUP_ManyTestTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_ManyTest.Text != "") number = Convert.ToInt32(NUDTextBox_ManyTest.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox_ManyTest.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_ManyTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_ManyTest.Text != "") number = Convert.ToInt32(NUDTextBox_ManyTest.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox_ManyTest.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_ManyTest_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP_ManyTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_ManyTest, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown_ManyTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_ManyTest, new object[] { true });
            }
        }

        private void NUDTextBox_ManyTest_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_ManyTest, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_ManyTest, new object[] { false });
        }

        private void NUDTextBox_ManyTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox_ManyTest.Text != "")
                if (!int.TryParse(NUDTextBox_ManyTest.Text, out number)) NUDTextBox_ManyTest.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox_ManyTest.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox_ManyTest.Text = minvalue.ToString();
            NUDTextBox_ManyTest.SelectionStart = NUDTextBox_ManyTest.Text.Length;

        }

        private void manyTestMode_Page1_Start_Click(object sender, RoutedEventArgs e)
        {
            ManyTestBuilder ManyTestBuilder = new ManyTestBuilder();
            ManyTestBuilder.CollectTestInfo(Int32.Parse(NUDTextBox_ManyTest.Text), (bool)addAdjectives_ManyTest_cb.IsChecked, (bool)nativeLanguage_ManyTest_cb.IsChecked);
            ManyTestBuilder.PrepareList();
            manyTest = ManyTestBuilder.GetTest();

            ManyTestShowQuestion();

            manyTestMode_Page1.Visibility = Visibility.Collapsed;
            manyTestMode_Page2.Visibility = Visibility.Visible;
        }

        private void ManyTestShowQuestion()
        {
            manyTestMode_pb.Value = (int)(((double)questionIndex / manyTest.questions.Count) * 100.0);
            manyTestMode_Page2_answer1.IsChecked = false;
            manyTestMode_Page2_answer2.IsChecked = false;
            manyTestMode_Page2_answer3.IsChecked = false;
            manyTestMode_Page2_answer4.IsChecked = false;
            if (manyTest.nativeLanguage)
            {
                manyTestMode_Page2_wordToTranslate_l.Content = manyTest.questions.ElementAt(questionIndex).GetCategory();
                Answer answers = manyTest.answers.ElementAt(questionIndex);
                manyTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetPolish();
                manyTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetPolish();
                manyTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetPolish();
                manyTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetPolish();
            }
            else
            {
                manyTestMode_Page2_wordToTranslate_l.Content = manyTest.questions.ElementAt(questionIndex).GetCategory();
                Answer answers = manyTest.answers.ElementAt(questionIndex);
                manyTestMode_Page2_answer1.Content = answers.answers.ElementAt(0).GetEnglish();
                manyTestMode_Page2_answer2.Content = answers.answers.ElementAt(1).GetEnglish();
                manyTestMode_Page2_answer3.Content = answers.answers.ElementAt(2).GetEnglish();
                manyTestMode_Page2_answer4.Content = answers.answers.ElementAt(3).GetEnglish();
            }
        }
        private void ManyTestCheckAnswer()
        {
            bool checked1 = (bool)manyTestMode_Page2_answer1.IsChecked;
            bool checked2 = (bool)manyTestMode_Page2_answer2.IsChecked;
            bool checked3 = (bool)manyTestMode_Page2_answer3.IsChecked;
            bool checked4 = (bool)manyTestMode_Page2_answer4.IsChecked;
            String answers = "";
            if (checked1)
                answers += "0";
            if (checked2)
                answers += "1";
            if (checked3)
                answers += "2";
            if (checked4)
                answers += "3";
            if (answers != "")
                manyTest.CheckAnswer(questionIndex - 1, answers);
        }
        private void manyTestMode_Page2_Next_Click(object sender, RoutedEventArgs e)
        {
            questionIndex++;
            if (questionIndex < manyTest.questions.Count)
            {
                ManyTestCheckAnswer();
                ManyTestShowQuestion();
            }
            else
            {
                ManyTestCheckAnswer();
                questionIndex = 0;
                manyTestMode_Page3_summary_l.Content = manyTest.ShowScore();
                manyTestMode_Page2.Visibility = Visibility.Collapsed;
                manyTestMode_Page3.Visibility = Visibility.Visible;
            }
        }

        private bool JustChecked1;
        private void manyTestMode_Page2_answer1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            JustChecked1 = true;
        }

        private void manyTestMode_Page2_answer1_Clicked(object sender, RoutedEventArgs e)
        {
            if (JustChecked1)
            {
                JustChecked1 = false;
                e.Handled = true;
                return;
            }
            RadioButton s = (RadioButton)sender;
            if ((bool)s.IsChecked)
                s.IsChecked = false;
        }
        private bool JustChecked2;
        private void manyTestMode_Page2_answer2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            JustChecked2 = true;
        }

        private void manyTestMode_Page2_answer2_Clicked(object sender, RoutedEventArgs e)
        {
            if (JustChecked2)
            {
                JustChecked2 = false;
                e.Handled = true;
                return;
            }
            RadioButton s = (RadioButton)sender;
            if ((bool)s.IsChecked)
                s.IsChecked = false;
        }
        private bool JustChecked3;
        private void manyTestMode_Page2_answer3_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            JustChecked3 = true;
        }

        private void manyTestMode_Page2_answer3_Clicked(object sender, RoutedEventArgs e)
        {
            if (JustChecked3)
            {
                JustChecked3 = false;
                e.Handled = true;
                return;
            }
            RadioButton s = (RadioButton)sender;
            if ((bool)s.IsChecked)
                s.IsChecked = false;
        }
        private bool JustChecked4;
        private void manyTestMode_Page2_answer4_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton s = (RadioButton)sender;
            JustChecked4 = true;
        }

        private void manyTestMode_Page2_answer4_Clicked(object sender, RoutedEventArgs e)
        {
            if (JustChecked4)
            {
                JustChecked4 = false;
                e.Handled = true;
                return;
            }
            RadioButton s = (RadioButton)sender;
            if ((bool)s.IsChecked)
                s.IsChecked = false;
        }

        private void manyTestMode_Page3_End_Click(object sender, RoutedEventArgs e)
        {
            manyTestMode_Page3.Visibility = Visibility.Collapsed;
            manyTestMode_Page1.Visibility = Visibility.Visible;
            menu_tc.SelectedIndex = 0;
        }
        //-----------------------------end----------------------------


        //-----------------------------test wpisz słowo----------------------------
        //obsługa numberpickera
        private void NUDButtonUP_WriteTestTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_WriteTest.Text != "") number = Convert.ToInt32(NUDTextBox_WriteTest.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox_WriteTest.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_WriteTest_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox_WriteTest.Text != "") number = Convert.ToInt32(NUDTextBox_WriteTest.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox_WriteTest.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_WriteTest_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP_WriteTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_WriteTest, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown_WriteTest.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_WriteTest, new object[] { true });
            }
        }

        private void NUDTextBox_WriteTest_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP_WriteTest, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown_WriteTest, new object[] { false });
        }

        private void NUDTextBox_WriteTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox_WriteTest.Text != "")
                if (!int.TryParse(NUDTextBox_WriteTest.Text, out number)) NUDTextBox_WriteTest.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox_WriteTest.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox_WriteTest.Text = minvalue.ToString();
            NUDTextBox_WriteTest.SelectionStart = NUDTextBox_WriteTest.Text.Length;

        }

        private void writeTestMode_Page1_Start_Click(object sender, RoutedEventArgs e)
        {
            WriteTestBuilder WriteTestBuilder = new WriteTestBuilder();
            WriteTestBuilder.CollectTestInfo(Int32.Parse(NUDTextBox_WriteTest.Text), (bool)addAdjectives_WriteTest_cb.IsChecked, (bool)nativeLanguage_WriteTest_cb.IsChecked);
            WriteTestBuilder.PrepareList();
            writeTest = WriteTestBuilder.GetTest();

            WriteTestShowQuestion();

            writeTestMode_Page1.Visibility = Visibility.Collapsed;
            writeTestMode_Page2.Visibility = Visibility.Visible;
        }

        private void WriteTestShowQuestion()
        {
            writeTestMode_pb.Value = (int)(((double)questionIndex / writeTest.questions.Count) * 100.0);
            writeTestMode_Page2_writeWord.Text = "";
            if (writeTest.nativeLanguage)
            {
                writeTestMode_Page2_wordToTranslate_l.Content = writeTest.questions.ElementAt(questionIndex).GetPolish();
            }
            else
            {
                writeTestMode_Page2_wordToTranslate_l.Content = writeTest.questions.ElementAt(questionIndex).GetEnglish();
            }
        }
        private void WriteTestCheckAnswer()
        {
            if (writeTestMode_Page2_writeWord.Text != "")
                writeTest.CheckAnswer(questionIndex - 1, writeTestMode_Page2_writeWord.Text);
        }
        private void writeTestMode_Page2_Next_Click(object sender, RoutedEventArgs e)
        {
            questionIndex++;
            if (questionIndex < writeTest.questions.Count)
            {
                WriteTestCheckAnswer();
                WriteTestShowQuestion();
            }
            else
            {
                WriteTestCheckAnswer();
                questionIndex = 0;
                writeTestMode_Page3_summary_l.Content = writeTest.ShowScore();
                writeTestMode_Page2.Visibility = Visibility.Collapsed;
                writeTestMode_Page3.Visibility = Visibility.Visible;
            }
        }
        private void writeTestMode_Page3_End_Click(object sender, RoutedEventArgs e)
        {
            writeTestMode_Page3.Visibility = Visibility.Collapsed;
            writeTestMode_Page1.Visibility = Visibility.Visible;
            menu_tc.SelectedIndex = 0;
        }
        //-----------------------------end----------------------------

        //zamknięcie okna - zapisanie naszej bazy słówek
        private void Window_Closed(object sender, EventArgs e)
        {
            Program.SaveToFile();
        }
    }
}
