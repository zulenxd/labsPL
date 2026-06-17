open System
open System.Text

type Tree =
    | Empty
    | Node of string * Tree * Tree

let rec treeMap f tree =
    match tree with
    | Empty -> Empty
    | Node(v, l, r) -> Node(f v, treeMap f l, treeMap f r)

let rec insert v tree =
    match tree with
    | Empty -> Node(v, Empty, Empty)
    | Node(root, l, r) ->
        if v < root then Node(root, insert v l, r)
        else Node(root, l, insert v r)

let rec printTree t d side =
    match t with
    | Empty -> ()
    | Node(v, l, r) ->
        printfn "%s%s: %s" (String.replicate d "  ") side v
        printTree l (d + 1) "L"
        printTree r (d + 1) "R"

[<EntryPoint>]
let main _ =
    Console.OutputEncoding <- Encoding.UTF8
    Console.InputEncoding <- Encoding.UTF8

    printf "Количество строк: "
    let count = int (Console.ReadLine())

    let tree = 
        seq { 1 .. count } 
        |> Seq.fold (fun acc i -> 
            printf "Введите строку %d: " i
            insert (Console.ReadLine()) acc
        ) Empty

    printf "Символ для замены: "
    let ch = Console.ReadLine().[0]

    printfn "\nИсходное дерево:"
    printTree tree 0 "Root"

    let newTree = tree |> treeMap (fun s -> if s.Length > 0 then string ch + s.[1..] else s)

    printfn "\nИзмененное дерево:"
    printTree newTree 0 "Root"
    
    0