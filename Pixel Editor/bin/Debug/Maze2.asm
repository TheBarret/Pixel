;************************************
; Program: Continuous Maze Generator
; Version: 2
;************************************
clear | seed 0xff | mode #1 | jmp [init]

:x		0
:y		0
:index		0
:author	"Made by Barret"

:init	call [intro]
:main	random #11
	store [index]
	if [index] #0 call [empty]
	if [index] #1 call [f1]
	if [index] #2 call [f2]
	if [index] #3 call [f3]
	if [index] #4 call [f4]
	if [index] #5 call [f5]
	if [index] #6 call [f6]
	if [index] #7 call [f7]
	if [index] #8 call [f8]
	if [index] #9 call [f9]
	if [index] #10 call [f10]
	if [index] #11 call [f11]
	inc [x] #8
	ifl [x] #128 jmp [main]
	storev [x] #0
	push #0
	push #6
	call [@scroll]
	push #60
	call [@sleep]
	jmp [main]

:intro	push #20
	push #7
	push [author]
	call [@print]
	return
	
:f1	draw [x][y][a] | return
:f2	draw [x][y][b] | return
:f3	draw [x][y][c] | return
:f4	draw [x][y][d] | return
:f5	draw [x][y][e] | return
:f6	draw [x][y][f] | return
:f7	draw [x][y][g] | return
:f8	draw [x][y][h] | return
:f9	draw [x][y][i] | return
:f10	draw [x][y][j] | return
:f11	draw [x][y][k] | return
:empty	draw [x][y][n] | return

:a	.00000000.00000000.01010101.10101010.00000000.00000000
:b	.00010000.00001000.00010000.00001000.00010000.00001000
:c	.00011000.00011000.11111111.11111111.00011000.00011000
:d	.00000000.00000000.11111111.11111111.00011000.00011000
:e	.00011000.00011000.11111111.11111111.00000000.00000000
:f	.00000000.00000000.00011111.00011111.00011000.00011000
:g	.00000000.00000000.11111000.11111000.00011000.00011000
:h	.00011000.00011000.00011111.00011111.00000000.00000000
:i	.00011000.00011000.11111000.11111000.00000000.00000000
:j	.00011000.00011000.11111000.11111000.00011000.00011000
:k	.00011000.00011000.00011111.00011111.00011000.00011000
:n	.00000000.00000000.00011000.00011000.00000000.00000000
