module App

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System

let canvas = document.querySelector(".view") :?> HTMLCanvasElement
let ctx = canvas.getContext_2d()
let mutable startDragX = 0.
let mutable dragX = 0.
let dragY = 0.
let mutable dragCanvas = false

let drawLinesGroup(stroke: String, posy: float) =
    let width = window.innerWidth
    let numLines = int (width / 10.) - 3
    let currOffset = dragX - startDragX
    for i in 1 .. numLines do
        ctx.beginPath()
        ctx.strokeStyle <- !^stroke
        ctx.lineWidth <- 2.
        ctx.moveTo((50. * float i) + currOffset, posy)
        ctx.lineTo((50. * float i) + currOffset, posy + 100.)
        ctx.stroke()

let drawAllGroups() =
    drawLinesGroup("#FF0000", 0.)
    drawLinesGroup("#00FF00", 100.)
    drawLinesGroup("#0000FF", 200.)
    drawLinesGroup("#FFFF00", 300.)
    drawLinesGroup("#00FFFF", 400.)
    drawLinesGroup("#FF00FF", 500.)
    drawLinesGroup("#FFFFFF", 600.)

let onres(e:UIEvent, canvas:HTMLCanvasElement) =
    canvas.height <- window.innerHeight - 20.
    canvas.width <- window.innerWidth - 20.

let onmousedown(e:MouseEvent) =
    startDragX <- e.clientX
    dragCanvas <- true

let onmouseup(e:MouseEvent) =
    dragX <- 0.
    startDragX <- 0.
    dragCanvas <- false

let onmousemove(e:MouseEvent) =
    if dragCanvas then
        dragX <- e.clientX


let rec gameLoop(dt: float) =
    ctx.fillStyle <- !^"#111111"
    ctx.fillRect(0., 0., window.innerWidth, window.innerHeight)
    drawAllGroups()
    window.requestAnimationFrame(gameLoop) |> ignore

let init() =
    window.addEventListener_resize(fun e -> onres(e, canvas))
    window.addEventListener_mousedown(fun e -> onmousedown(e))
    window.addEventListener_mousemove(fun e -> onmousemove(e))
    window.addEventListener_mouseup(fun e -> onmouseup(e))
    canvas.height <- window.innerHeight - 20.
    canvas.width <- window.innerWidth - 20.
    gameLoop 0.


init()