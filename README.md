# Курсовой проект C#
*Описание приложения:* API с использованием стека технологий ASP.NET и Android-приложение на языке Kotlin, осуществляющее шифровку и дешифровку текста методом Вижинера.

**<div style="text-align:center">API</div>** 
https://github.com/nikaalin/FLS_course/tree/coursework 

*Функционал:*
-  Возможность шифрования/дешифровывания текста с указанием ключа шифрования из файлов .txt и .docx, а также текстового запроса
-  Запрос для шифрования: "http://{host}/VigenereCipherMew/api/Encrypt?key={key}&format={format}"
-  Запрос для дешифрования: "http://{host}/VigenereCipherMew/api/Decrypt?key={key}&format={format}"
{host}: адрес, на котором запущено api
{format}: одно из значений ["docx", "txt", "string"]
{key}: ключ используемый для шифрования/дешифрования

Основной функционал программы покрыт тестами, которые находятся в проекте "VigenereCipherTests".

*Запуск:*

- Включить в Windows IIS. *Туториал для Windows http://pyatilistnik.org/how-to-install-iis-on-windows-10/*
- Изменить Visual Studio с помощью Installer. Включить поддержку времени разработки в IIS
- Скачать исходный код и открыть .sln файл в Visual Studio с правами администратора
- Запустить проект через profile "Application START" 

**<div style="text-align:center">Android-приложение</div>** 
https://github.com/nikaalin/FLS_course/tree/coursework-android:

*Функционал:*
-  Возможность шифрования/дешифровывания текста (через запросы к API) из .docx/.text файлов, а также введенного в ручную
-  Возможность задания ключа для шифрования/дешифровывания
-  Возможность сохранения результата в формате .txt с указанием имени 
*(Сохраненные файлы находятся во внутренней памяти в директории Android/data/com.example.cipher2/files/txt)*

*Запуск:*
- Предварительно скачать и запустить API https://github.com/nikaalin/FLS_course/tree/coursework 
- Для запуска проекта необходимо скачать исходный код и открыть проект в Android Studio последней версии.
- Изменить значение ip на адрес хоста API в файле /app/src/main/java/com/example/cipher2/ProjectValues.kt. 
  *Адрес хоста можно узнать, запустив утилиту ipconfig в командной строке*
- Запустить приложение на реальном устройстве/эмуляторе с ОС Android 8.0 или выше.

