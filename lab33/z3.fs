open System
open System.Text
open System.IO 

let rec readDirectory prompt =
    printf "%s" prompt
    let path = Console.ReadLine()

    if Directory.Exists(path) then
        path
    else
        printfn "Ошибка: указанный каталог не существует. Попробуйте еще раз."
        readDirectory prompt

[<EntryPoint>]
let main _ =

    Console.OutputEncoding <- Encoding.UTF8
    Console.InputEncoding <- Encoding.UTF8

    let dirPath = readDirectory "Введите путь к каталогу: "

    let files = Directory.GetFiles(dirPath)

    if files.Length = 0 then
         printfn "\nВ указанном каталоге нет файлов."
    else
         let longestFile =
             files
             |> Seq.map (fun f -> Path.GetFileName(f)) 
             |> Seq.maxBy (fun name -> String.length name)

         printfn "\nСамое длинное название файла: \"%s\"" longestFile
         printfn "Его длина: %d символов" (String.length longestFile)
  
    0
