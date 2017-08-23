;************************************
; Program: Credits
;************************************
clear | jmp [main]

:main
	call [draw_line]
	call [draw_text]
	call [pointer]
	jmp [main]

:draw_line
	ifg [lx] #120 return
	draw [lx][ly][line]
	inc [lx] #8
	jmp [draw_line]

:draw_text
	strlen [intro][tlen]
	ifv [ti][tlen]  return
	print [tx][ty][ti][intro]
	inc [ti] #1
	inc [tx] #5
	jmp [draw_text]	

:pointer
	input [key]
	if [key] #0 return
	ifv [key] [pu] call [pointer_up]
	ifv [key] [pd] call [pointer_down]
	ifv [key] [pl] call [pointer_left]
	ifv [key] [pr] call [pointer_right]
	call [pointer_draw]
	return

:pointer_draw
	draw [px][py][psprite]
	draw [px][py][psprite]
	return

:pointer_up
	ifl [py] #11 return
	load [py]
	load [step]
	sub
	store [py]
	return

:pointer_down
	ifg [py] #60 return
	load [py]
	load [step]
	add
	store [py]
	return

:pointer_left
	iflv [px] [step] return
	load [px]
	load [step]
	sub
	store [px]
	return

:pointer_right
	ifg [px] #120 return
	load [px]
	load [step]
	add
	store [px]
	return

:px		10
:py		15
:lx		0
:ly		8
:key		0
:step		2
:tx		0
:ty		1
:ti		0
:tlen	0
:pu		{W}
:pd		{S}
:pl		{A}
:pr		{D}
:intro		"Made by Barret [c] 2017"

:psprite
	.11110000
	.11000000
	.10100000
	.10010000
	.00001000
	.00000000
:line
	.11111111
	.11111111
	.00000000
	.00000000
	.00000000
	.00000000