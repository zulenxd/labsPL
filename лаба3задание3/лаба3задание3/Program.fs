open System
open System.IO

[<EntryPoint>]
let main args =
    printf "Введите путь к каталогу: "
    let dirPath = Console.ReadLine()

    
        // 1. Получаем ленивую последовательность путей ко всем файлам в каталоге
    let filesSeq = Directory.EnumerateFiles(dirPath)

        // Проверяем, есть ли вообще файлы в папке, чтобы Seq.maxBy не выдал ошибку
    if Seq.isEmpty filesSeq then
            printfn "В указанном каталоге нет файлов."
    else
            // 2. Обрабатываем последовательность через конвейер
        let longestFile = 
            filesSeq
                // Берем только имя файла (отрезаем полный путь)
            |> Seq.map Path.GetFileName
                // Находим файл с максимальной длиной имени
            |> Seq.maxBy String.length

        printfn "Самое длинное название файла: %s" longestFile
        printfn "Длина названия: %d символов" longestFile.Length
    0