# Къса памет :crystal_ball:

> Невил Лонгботъм е кръглолико момче от „Грифиндор“, което все губи или забравя нещо.

Задачата ни днес е да напишем кратък метод, който да напомня на Невил да записва в тефтерче телефонните номера, които най-вероятно ще забрави. Такива телефонни номера са

- тези без никакви повтарящи се цифри
- тези със символи, различни от цифри

### Условие

Създайте публичен клас `Remembrall` и имплементирайте следния негов метод:

```java
public static boolean isPhoneNumberForgettable(String phoneNumber)
```

Методът приема телефонен номер под формата на символен низ и връща `true`, ако е труден за помнене и `false` в противен случай.
Телефонният номер може да съдържа цифри и латински букви, които могат да бъдат разделени с " " или "-".
В случай че номерът е *празен*, връщайте `false` - не можем да забравим нещо, което го няма.

### Примери

| Извикване                                   | Резултат |
| :------------------------------------------ | :------- |
| `isPhoneNumberForgettable("")`              | false    |
| `isPhoneNumberForgettable("498-123-123")`   | false    |
| `isPhoneNumberForgettable("0894 123 567")`  | true     |
| `isPhoneNumberForgettable("(888)-FLOWERS")` | true     |
| `isPhoneNumberForgettable("(444)-greens")`  | true     |

### :warning: Забележки

- Не се очаква проверка за специални символи (например +, *, %, ь, ...)
- Използването на структури от данни, различни от масив, **не е позволено**. Задачата трябва да се реши с помощта на знанията от първата лекция от курса - иначе няма да бъде интересна


# Tic-Tac-Toe
### Description

This is a classical Tic-Tac-Toe game. It has 2 modes:
* "1 Player" mode - You choose to play against PC on medium level
* "2 Players" mode - You choose to play with another person, taking turns

<p align="center">
  <img src="/Screenshots/3-ModeWindow.png" width="300">
</p>

Before start, players should type nicknames which lengths can be maximum 10 characters.
* If selected mode is "1 Player", then Player 2 is the PC and its nickname is "PC". It could not be changed
* If selected mode is "2 Players", then players should type different nicknames

<p align="center">
  <img src="/Screenshots/6-ErrorLongNames.png" width="400"> </br>
  <img src="/Screenshots/7-ErrorSameNames.png" width="400">
</p>

After clicking button "Start", it is replaced by button "Again" and button "Restart" appears </br>
**Button "Again"** - It starts new game between Player 1 and Player 2 without resetting current result </br>
**Button "Restart"** - First, it saves current result in "Result.xlsx" file which is placed in /Debug/bin folder. </br>
Then, it restarts current result and starts new game between Player 1 and Player 2

<p align="center">
  <img src="/Screenshots/9-SavingResult.png" width="400"> </br>
  <img src="/Screenshots/SavedResults.png" width="400">
</p>

**_You can see more images in "Screenshots". </br>
Even more, you can test the application after downloading TicTacToe.exe from /TicTacToe/Debug/bin folder_**
