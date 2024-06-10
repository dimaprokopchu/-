using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex;
        private int score;
        private List<string> userAnswers;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuestions();
            ShowMainMenu();
        }

        private void LoadQuestions()
        {
            var historyQuestions = new List<Question>
            {
                new Question("Хто був першим президентом України?", "Леонід Кравчук", new List<string> { "Віктор Ющенко", "Леонід Кучма", "Володимир Зеленський", "Петро Порошенко" }),
                new Question("У якому році була проголошена незалежність України?", "1991", new List<string> { "1989", "1992", "1993", "1990" }),
                new Question("Яка подія відбулася 22 січня 1919 року?", "Акт Злуки", new List<string> { "Голодомор", "Чорнобильська катастрофа", "Помаранчева революція", "Бій під Крутами" }),
                new Question("Хто був автором Конституції Пилипа Орлика?", "Пилип Орлик", new List<string> { "Богдан Хмельницький", "Іван Мазепа", "Михайло Грушевський", "Іван Франко" }),
                new Question("У якому році відбулася Чорнобильська катастрофа?", "1986", new List<string> { "1984", "1985", "1987", "1988" }),
                new Question("Хто був головним отаманом військ УНР?", "Симон Петлюра", new List<string> { "Михайло Грушевський", "Іван Мазепа", "Богдан Хмельницький", "Петро Дорошенко" }),
                new Question("Яка річка є найдовшою в Україні?", "Дніпро", new List<string> { "Дністер", "Південний Буг", "Сіверський Донець", "Тиса" }),
                new Question("Хто є автором національного гімну України?", "Павло Чубинський", new List<string> { "Тарас Шевченко", "Леся Українка", "Іван Франко", "Микола Лисенко" }),
                new Question("Яке місто було столицею Галицько-Волинського князівства?", "Галич", new List<string> { "Львів", "Тернопіль", "Івано-Франківськ", "Ужгород" }),
                new Question("Хто був першим гетьманом Війська Запорозького?", "Дмитро Вишневецький", new List<string> { "Богдан Хмельницький", "Іван Мазепа", "Петро Сагайдачний", "Петро Дорошенко" })
            };

            var languageQuestions = new List<Question>
            {
                new Question("Що таке дієслово?", "Частина мови, що означає дію або стан.", new List<string> { "Частина мови, що означає предмет.", "Частина мови, що означає ознаку предмета.", "Частина мови, що означає кількість.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке іменник?", "Частина мови, що означає предмет.", new List<string> { "Частина мови, що означає дію.", "Частина мови, що означає ознаку предмета.", "Частина мови, що означає кількість.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке прикметник?", "Частина мови, що означає ознаку предмета.", new List<string> { "Частина мови, що означає предмет.", "Частина мови, що означає дію.", "Частина мови, що означає кількість.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке числівник?", "Частина мови, що означає кількість або порядок предметів при лічбі.", new List<string> { "Частина мови, що означає предмет.", "Частина мови, що означає дію.", "Частина мови, що означає ознаку предмета.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке займенник?", "Частина мови, що вказує на предмет, ознаку або кількість, не називаючи їх.", new List<string> { "Частина мови, що означає предмет.", "Частина мови, що означає дію.", "Частина мови, що означає кількість.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке прислівник?", "Частина мови, що означає ознаку дії, стану або іншої ознаки.", new List<string> { "Частина мови, що означає предмет.", "Частина мови, що означає дію.", "Частина мови, що означає кількість.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке сполучник?", "Частина мови, що служить для зв'язку слів і речень.", new List<string> { "Частина мови, що означає ознаку предмета.", "Частина мови, що означає дію.", "Частина мови, що означає предмет.", "Частина мови, що означає обставини дії." }),
                new Question("Що таке прийменник?", "Частина мови, що служить для вираження відношень між словами в реченні.", new List<string> { "Частина мови, що означає ознаку предмета.", "Частина мови, що означає дію.", "Частина мови, що означає предмет.", "Частина мови, що означає обставини дії." })
            };

            questions = historyQuestions.Concat(languageQuestions).ToList();

            foreach (var question in questions)
            {
                question.Answers.Add(question.CorrectAnswer);
                question.ShuffleAnswers();
            }
        }

        private void ShowMainMenu()
        {
            MainMenuPanel.Visibility = Visibility.Visible;
            QuizPanel.Visibility = Visibility.Collapsed;
            ResultsPanel.Visibility = Visibility.Collapsed;
            ReturnToMenuButton.Visibility = Visibility.Collapsed;
            currentQuestionIndex = 0;
            score = 0;
        }

        private void StartHistoryQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz(questions.Where(q => q.IsHistoryQuestion).ToList());
        }

        private void StartLanguageQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz(questions.Where(q => !q.IsHistoryQuestion).ToList());
        }

        private void StartQuiz(List<Question> selectedQuestions)
        {
            questions = selectedQuestions;
            userAnswers = new List<string>(new string[questions.Count]);
            score = 0;
            currentQuestionIndex = 0;
            DisplayQuestion();
            MainMenuPanel.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Visible;
            ReturnToMenuButton.Visibility = Visibility.Visible;
            ResultsPanel.Visibility = Visibility.Collapsed;
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                QuestionNumber.Text = $"Питання {currentQuestionIndex + 1} з {questions.Count}";
                QuestionText.Text = question.Text;
                AnswersList.ItemsSource = question.Answers;
                FeedbackText.Text = "";
                AnswersList.SelectedItem = userAnswers[currentQuestionIndex];
            }
            else
            {
                ShowResults();
            }
        }

        private void ShowResults()
        {
            QuestionNumber.Text = "Тест завершено!";
            QuestionText.Text = "";
            AnswersList.Visibility = Visibility.Collapsed;
            FeedbackText.Text = $"Ви отримали {score} балів з {questions.Count} можливих.\n";
            ResultsDataGrid.ItemsSource = questions.Select((q, index) => new ResultRow
            {
                QuestionNumber = index + 1,
                QuestionText = q.Text,
                YourAnswer = userAnswers[index],
                CorrectAnswer = q.CorrectAnswer,
                IsCorrect = userAnswers[index] == q.CorrectAnswer ? "Так" : "Ні"
            }).ToList();
            ResultsPanel.Visibility = Visibility.Visible;
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersList.SelectedItem != null)
            {
                var selectedAnswer = AnswersList.SelectedItem.ToString();
                var correctAnswer = questions[currentQuestionIndex].CorrectAnswer;

                if (selectedAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    FeedbackText.Text = "Правильно!";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Green);
                    if (userAnswers[currentQuestionIndex] != correctAnswer) score++;
                }
                else
                {
                    FeedbackText.Text = $"Неправильно. Правильна відповідь: {correctAnswer}";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Red);
                    if (userAnswers[currentQuestionIndex] == correctAnswer) score--;
                }

                userAnswers[currentQuestionIndex] = selectedAnswer;
                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                FeedbackText.Text = "Виберіть відповідь.";
                FeedbackText.Foreground = new SolidColorBrush(Colors.Yellow);
            }
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayQuestion();
            }
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            ShowMainMenu();
        }
    }

    public class Question
    {
        public string Text { get; }
        public string CorrectAnswer { get; }
        public List<string> Answers { get; }
        public bool IsHistoryQuestion => Text.Contains("Хто") || Text.Contains("коли") || Text.Contains("яке") || Text.Contains("Що");

        public Question(string text, string correctAnswer, List<string> wrongAnswers)
        {
            Text = text;
            CorrectAnswer = correctAnswer;
            Answers = new List<string>(wrongAnswers);
        }

        public void ShuffleAnswers()
        {
            Random rng = new Random();
            int n = Answers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = Answers[k];
                Answers[k] = Answers[n];
                Answers[n] = value;
            }
        }
    }

    public class ResultRow
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string YourAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public string IsCorrect { get; set; }
    }
}
