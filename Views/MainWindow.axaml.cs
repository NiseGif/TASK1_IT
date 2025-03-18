using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Queue.Views
{
    public partial class MainWindow : Window 
    {
        private Models.Queue<string> _queue;

        public MainWindow()
        {
            InitializeComponent();
            _queue = new Models.Queue<string>();
        }
        
        // Метод для добавления элемента в очередь
        private void OnEnqueueButtonClick(object sender, RoutedEventArgs e)
        {
            AddItemToQueue();
        }

        // Метод для обработки нажатия клавиши
        private void NewItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // Проверяем, нажата ли клавиша Enter
            {
                AddItemToQueue();
            }
        }

        // Метод для добавления элемента в очередь
        private void AddItemToQueue()
        {
            if (NewItemTextBox.Text != null)
            {
                string newItem = NewItemTextBox.Text;
                if (!string.IsNullOrWhiteSpace(newItem))
                {
                    _queue.Enqueue(newItem);
                    NewItemTextBox.Text = string.Empty; // Очищаем текстовое поле
                    DisplayQueue();
                }
                else
                {
                    ShowNotification("Введите корректное значение."); 
                }
            }
        }

        // Метод для отображения текущих элементов очереди
        private void DisplayQueue()
        {
            QueueItemsTextBlock.Text = string.Join(", ", _queue.GetAllItems());
        }

        // Метод для удаления первого элемента из очереди
        private void OnDequeueButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_queue.IsEmpty)
            {
                _queue.Dequeue(); 
                DisplayQueue();
                ShowNotification("Элемент удалён из очереди!");
            }
            else
            {
                ShowNotification("В очереди пока нет элементов!"); // Метод для отображения сообщения
            }
        }

        // Метод для очистки очереди (если нужно)
        private void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            _queue.Clear();
            if (_queue.IsEmpty)
            {
                ShowNotification("Очередь пуста!");
                DisplayQueue();
            }
            
        }
        
        private void ShowNotification(string message)
        {
            // Обновляем текст и видимость UI элемента в основном потоке
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                NotificationTextBlock.Text = message;
                NotificationTextBlock.IsVisible = true;
            });

            // Скрываем уведомление через 3 секунды
            var timer = new System.Threading.Timer((e) =>
            {
                // Скрываем уведомление через 3 секунды в основном потоке
                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    NotificationTextBlock.IsVisible = false;
                });
            }, null, 2000, System.Threading.Timeout.Infinite);
        }
    }
}
