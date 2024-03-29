# Compiler
## Лабораторная работа №1 по дисциплине Теория формальных языков и компиляторов. GUI

Создать

![Создать](images/image.png)

Открыть

![Открыть](images/image-1.png)

Сохранить

![Сохранить](images/image-2.png)

Отмена

![Отмена](images/image-3.png)

Копировать

![Копировать](images/image-4.png)

Вырезать

![Вырезать](images/image-5.png)

Вставить

![Вставить](images/image-6.png)

Пуск

![Пуск](images/image-7.png)

Справка

![Справка](images/image-8.png)

О программе

![О программе](images/image-9.png)

Общний внешний вид программы
![Внешний вид](images/image-10.png)

Пример локализации ru_RU
![Пример локализации ru_RU](images/image-11.png)

Пример локализации zn_CN
![Пример локализации zn_CN](images/image-12.png)

## Лабораторная работа №2. Разработка лексического анализатора.

Тема: Разработка лексического анализатора (сканера).
Цель работы: Изучить назначение лексического анализатора. Спроектировать алгоритм и выполнить программную реализацию сканера.

Вариант: Объявление комплексного числа с инициализацией на языке C++
Пример входной строки: std::complex<double> comp(10.0, 123.0);

В соответствии с вариантом задания необходимо:

1. Спроектировать диаграмму состояний сканера (примеры диаграмм представлены в прикрепленных файлах).
2. Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми (выводить ошибку).
3. Встроить сканер в ранее разработанный интерфейс текстового редактора. Учесть, что текст для разбора может состоять из множества строк.

Входные данные: строка (текст программного кода).
Выходные данные: последовательность условных кодов, описывающих структуру разбираемого текста с указанием места положения и типа.

```C++
    std::complex<double> comp(10.0,0.2);
```

### Диаграмме состояний сканера
![Картинка](images/scheme.png)

### Примеры лексического анализа
![О боже где картинка](images/test.png)

## Лабораторная работа №3. Разработка парсера.
Цель работы: Изучить назначение синтаксического анализатора. Спроектировать алгоритм и выполнить программную реализацию парсера.

### Грамматика
G[COMPLEX = <комплексное число>]: 

VT = { ‘a’…’z’, ‘A’…’Z’, '=', 'std::complex<double>', '(', ')', '+', 'j', '-', '0'...'9', '.', ‘_’ }

VN = { COMPLEX, IDENTIFIER, IDENTIFIERREM, REAL, UINTREAL, UINTREALREM, REALDECIMAL, REALDECIMALREM, IMAGINARY, UINTINAGINARY, UINTIMAGINARYREM, IMANGINARYDECIMAL,END, letter, digit }

COMPLEX -> 'std::complex<double> 'IDENTIFIER
IDENTIFIER -> (letter | '_')IDENTIFIERREM
IDENTIFIERREM -> (letter | digit | '_')IDENTIFIERREM | '('REAL
REAL -> ['+' | '-']UINTREAL
UINTREAL -> digit UINTREALREM
UINTREALREM -> digit UINTREALREM | '.' REALDECIMAL
REALDECIMAL -> digit REALDECIMALREM
REALDECIMALREM -> digit REALDECIMALREM | ',' IMAGINARY
IMAGINARY -> ['+' | '-']UINTINAGINARY
UINTINAGINARY -> digit UINTIMAGINARYREM
UINTIMAGINARYREM -> digit UINTIMAGINARYREM | '.' IMANGINARYDECIMAL
IMAGINARYDECIMAL -> digit IMAGINARYDECIMAL | ')' END
END -> ';'
digit -> '0' | '1' | '2' | ... | '9'
letter -> 'a' | 'b' | ... | 'z' | 'A' | 'B' | ... | 'Z' 

### Граф конечного автомата
![Я хочу быть автоматом](images/automaton.jpg)