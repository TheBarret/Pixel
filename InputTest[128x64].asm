;************************************
; Program: Input & Collision text
;************************************
clear | jmp [init]

:up			{W}			;define keys
:down			{S}
:right			{D}
:left			{A}
:exit			{Q}
:tx			1
:ty			1
:ti			0
:key		0				;key input buffer
:x			64
:y			32
:d			2
:s			8
:c			0
:l			0
:info			"Use 'WASD' keys..."

:init
	strlen [info][l]
	ifv [ti][l] jmp [main]
	print [tx][ty][ti][info]
	inc [ti] #1
	inc [tx] #5
	jmp [init]
:main
	input [key]					;fetch key buffer
	call [worker]
	stcol [c]					;store collision flag
	if [c] #1 clear
	jmp [main]

:worker
	ifv [key] [up] call [draw_up]
	ifv [key] [down] call [draw_down]
	ifv [key] [right] call [draw_right]
	ifv [key] [left] call [draw_left]
	ifv [key] [exit] end
	return
:draw_up
	storev [d] #0
	scroll [d][s]
	draw [x][y][arrow_up]
	return
:draw_down
	storev [d] #1
	scroll [d][s]
	draw [x][y][arrow_down]
	return
:draw_right
	storev [d] #3
	scroll [d][s]
	draw [x][y][arrow_right]
	return
:draw_left
	storev [d] #2
	scroll [d][s]
	draw [x][y][arrow_left]
	return

:arrow_up
		.00010000
		.00111000
		.01111100
		.00010000
		.00010000
		.00010000
:arrow_down	
		.00010000
		.00010000
		.00010000
		.01111100
		.00111000
		.00010000
:arrow_right
		.00000000
		.00010000
		.00011000
		.11111100
		.00011000
		.00010000
:arrow_left	
		.00000000
		.00100000
		.01100000
		.11111100
		.01100000
		.00100000