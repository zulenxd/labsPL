// Задание 3. Вариант 4.
// Вывести самое длинное название файла в указанном каталоге.

open System
open System.Text
open System.IO // Обязательно подключаем эту библиотеку для работы с файлами и папками

/// Безопасный ввод пути к каталогу (проверяем, существует ли он)
let rec readDirectory prompt =
    printf "%s" prompt
    let path = Console.ReadLine()
    // Проверяем, существует ли такая папка на компьютере
    if Directory.Exists(path) then
        path
    else
        printfn "Ошибка: указанный каталог не существует. Попробуйте еще раз."
        readDirectory prompt

[<EntryPoint>]
let main _ =

    Console.OutputEncoding <- Encoding.UTF8
    Console.InputEncoding <- Encoding.UTF8

    // 1. Запрашиваем путь к каталогу (например, C:\Windows или /usr/bin)
    let dirPath = readDirectory "Введите путь к каталогу: "

    
     // 2. Получаем массив полных путей ко всем файлам в этой папке
    let files = Directory.GetFiles(dirPath)

     // 3. Проверяем, не пуста ли папка
    if files.Length = 0 then
         printfn "\nВ указанном каталоге нет файлов."
    else
         // 4. Ищем самое длинное название файла
         let longestFile =
             files
             // Оставляем только имена файлов (отрезаем путь вроде "C:\...")
             |> Seq.map (fun f -> Path.GetFileName(f)) 
             // Находим максимальный элемент по длине строки
             |> Seq.maxBy (fun name -> String.length name)

         printfn "\nСамое длинное название файла: \"%s\"" longestFile
         printfn "Его длина: %d символов" (String.length longestFile)
            
  
    0
