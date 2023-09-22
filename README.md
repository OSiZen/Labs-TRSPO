# Туторіал по використанню моїх кодів
Використовуємо [Visual Studio Community](https://visualstudio.microsoft.com/vs/community) для створення проєктів

## Консольні проєкти

**Створюємо проєкт:** File – New – Project – Console App (.NET Framework)

**Додаємо клас:** Project – Add Class – <назва класу>.cs

**Вставляємо створений код:** View – Solution Explorer – <назва класу>.cs – вставляємо код програми в блок namespace <назва проєкту> та не забуваємо вставити using за потреби

**Запобігаємо виникненню помилки CS0017:** Project – <назва проєкту> Properties – Application – Startup object – <назва проєкту>.Program

*Текст помилки: Program has more than one entry point defined. Compile with /main to specify the type that contains the entry point*

**Запускаємо проєкт:** View – Solution Explorer – Program.cs – в блоці static void Main(string[] args) оголошуємо клас, який буде виконуватися при запуску <назва класу>.<назва методу класу>(<аргументи>); (наприклад, Task1.Main() та Task2.Main()) – F5 (з debug) або Ctrl+F5


## Проєкти з графічним інтерфейсом

**Створюємо проєкт:** File – New – Project – Windows Forms App (.NET Framework)

**Додаємо вікно (форму):** Project – Add Form (Windows Forms) – <назва форми>.cs

**Вставляємо створений код:**

View – Solution Explorer – розгортаємо <назва форми>.cs – <назва форми>.Designer.cs – *вставляємо код для дизайну вікна* в блок partial class <назва форми>

View – Solution Explorer – <назва форми>.cs – F7 (або ПКМ – View Code) – *вставляємо код програми* в блок public partial class <назва форми> : Form та не забуваємо вставити using за потреби

**Запускаємо проєкт:** View – Solution Explorer – Program.cs – змінюємо форму запуску Application.Run(new <назва форми>(<аргументи>)) – F5 (з debug) або Ctrl+F5