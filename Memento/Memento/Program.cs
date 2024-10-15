namespace Memento
{
    internal class Program
    {
        static void Main(string[] args) // Точка входу
        {
            Editor editor = new Editor(); // Створюємо екземпляр редактора
            Command command = new Command(editor); // Створюємо команду

            // Задаємо початковий текст
            editor.setText("Hello, World!");
            editor.setCursor(0, 0);
            editor.setSelectionWidth(0);
            command.makeBackup(); // Зберігаємо стан

            // Зміна тексту
            editor.setText("Hello, Universe!");
            editor.setCursor(0, 0);
            command.makeBackup(); // Зберігаємо новий стан

            // Відновлення до попереднього стану
            command.undo(); // Повертаємося до "Hello, World!"

            // Виведемо текст на консоль
            Console.WriteLine(editor.getText()); // Виведе: Hello, World!
        }
    }

    // Клас редактора
    class Editor
    {
        private string text; // Текст редактора
        private int curX; // Позиція курсора по X
        private int curY; // Позиція курсора по Y
        private int selectionWidth; // Ширина виділення

        public void setText(string text) // Метод для встановлення тексту
        {
            this.text = text;
        }

        public void setCursor(int x, int y) // Метод для встановлення позиції курсора
        {
            this.curX = x;
            this.curY = y;
        }

        public void setSelectionWidth(int width) // Метод для встановлення ширини виділення
        {
            this.selectionWidth = width;
        }

        // Метод для створення знімка (snapshot)
        public Snapshot createSnapshot()
        {
            return new Snapshot(this, text, curX, curY, selectionWidth);
        }

        // Метод для отримання тексту (для виведення)
        public string getText()
        {
            return text;
        }
    }

    // Клас знімка (snapshot)
    class Snapshot
    {
        private Editor editor; // Посилання на редактор
        private string text; // Текст
        private int curX; // Позиція курсора по X
        private int curY; // Позиція курсора по Y
        private int selectionWidth; // Ширина виділення

        // Конструктор для знімка
        public Snapshot(Editor editor, string text, int curX, int curY, int selectionWidth)
        {
            this.editor = editor;
            this.text = text;
            this.curX = curX;
            this.curY = curY;
            this.selectionWidth = selectionWidth;
        }

        // Метод для відновлення стану редактора
        public void restore()
        {
            editor.setText(text);
            editor.setCursor(curX, curY);
            editor.setSelectionWidth(selectionWidth);
        }
    }

    // Клас команди
    class Command
    {
        private Snapshot backup; // Знімок для відновлення
        private Editor editor; // Посилання на редактор

        // Конструктор для команди
        public Command(Editor editor)
        {
            this.editor = editor;
        }

        // Метод для збереження знімка
        public void makeBackup()
        {
            backup = editor.createSnapshot();
        }

        // Метод для відновлення стану
        public void undo()
        {
            if (backup != null)
                backup.restore();
        }
    }
}
