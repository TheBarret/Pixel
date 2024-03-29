;************************************
; Program: Continuous Maze Generator
;************************************
include common
clear | jmp [main]

:x		0
:y		0
:i		1

:main	random #4
	store [i]
	if [i] #0 call [left]
	if [i] #1 call [right]
	if [i] #2 call [both]
	if [i] #3 call [empty]
	inc [x] #8
	ifl [x] #128 jmp [main]
	storev [x] #0
	push #0
	push #6
	call [@scroll]
	jmp [main]

:left	draw [x][y][a] | return
:right	draw [x][y][b] | return
:both	draw [x][y][c] | return
:empty	draw [x][y][n] | return

:a	.00000000.00000000.00011000.00100100.01000010.10000001
:b	.10000001.01000010.00100100.00011000.00000000.00000000
:c	.10000001.01000010.00100100.00100100.01000010.10000001
:n	.00000000.00000000.00000000.00000000.00000000.00000000