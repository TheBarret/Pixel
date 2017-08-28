;************************************
; Program: Demo
;************************************

include print

clear | mode #1 | seed 0xaf | jmp [init]

:init	push #20
	push #25
	push [line1]
	call [@print]
	push #40
	push #35
	push [line2]
	call [@print]
	call [r1]
	call [r2]
	call [r3]
	call [r4]
:main	draw [x1][y1][star]
	draw [x2][y2][planet]
	draw [x3][y3][star]
	draw [x4][y4][planet]
	draw [x1][y1][star]
	draw [x2][y2][planet]
	draw [x3][y3][star]
	draw [x4][y4][planet]
	call [p1]
	call [p2]
	call [p3]
	call [p4]
	jmp [main]

;************************************
:x1	0
:y1	0
:p1	iflv [x1] [max_x] call [px11]
	ifgv [x1] [max_x] call [px12]
	ifv  [x1] [max_x] call [px12]
	iflv [y1] [max_y] call [py11]
	ifgv [y1] [max_y] call [py12]
	ifv  [y1] [max_y] call [py12]
	ifl [x1] #1 call [r1]
	ifg [x1] #128 call [r1]
	ifl [y1] #1 call [r1]
	ifg [y1] #64 call [r1]
	return
:r1	random #128
	store [x1]
	random #64
	store [y1]
	return
:px11	dec [x1] #1 |	return
:px12	inc [x1] #1 |	return
:py11	dec [y1] #1 |	return
:py12	inc [y1] #1 |	return

;************************************
:x2	0
:y2	0
:p2	iflv [x2] [max_x] call [px21]
	ifgv [x2] [max_x] call [px22]
	ifv  [x2] [max_x] call [px22]
	iflv [y2] [max_y] call [py21]
	ifgv [y2] [max_y] call [py22]
	ifv  [y2] [max_y] call [py22]
	ifl [x2] #1 call [r2]
	ifg [x2] #128 call [r2]
	ifl [y2] #1 call [r2]
	ifg [y2] #64 call [r2]
	return
:r2	random #128
	store [x2]
	random #64
	store [y2]
	return
:px21	dec [x2] #1 | return
:px22	inc [x2] #1 |	return
:py21	dec [y2] #1 | return
:py22	inc [y2] #1 | return

;************************************
:x3	0
:y3	0
:p3	iflv [x3] [max_x] call [px31]
	ifgv [x3] [max_x] call [px32]
	ifv  [x3] [max_x] call [px32]
	iflv [y3] [max_y] call [py31]
	ifgv [y3] [max_y] call [py32]
	ifv  [y3] [max_y] call [py32]
	ifl [x3] #1 call [r3]
	ifg [x3] #128 call [r3]
	ifl [y3] #1 call [r3]
	ifg [y3] #64 call [r3]
	return
:r3	random #128
	store [x3]
	random #64
	store [y3]
	return
:px31 	dec [x3] #1 | return
:px32	inc [x3] #1 | return
:py31	dec [y3] #1 | return
:py32	inc [y3] #1 |	return

;************************************
:x4	0
:y4	0
:p4	iflv [x4] [max_x] call [px41]
	ifgv [x4] [max_x] call [px42]
	ifv  [x4] [max_x] call [px42]
	iflv [y4] [max_y] call [py41]
	ifgv [y4] [max_y] call [py42]
	ifv  [y4] [max_y] call [py42]
	ifl [x4] #1 call [r4]
	ifg [x4] #128 call [r4]
	ifl [y4] #1 call [r4]
	ifg [y4] #64 call [r4]
	return
:r4	random #128
	store [x4]
	random #64
	store [y4]
	return
:px41 	dec [x4] #1 | return
:px42	inc [x4] #1 | return
:py41	dec [y4] #1 | return
:py42	inc [y4] #1 |	return

;************************************
; Variables
:max_x	64
:max_y	32
:line1	"Welcome to..."
:line2	"...Pixel"
:star	.00000000.00001000.00000000.00101010.00000000.00001000
:planet.00011000.00110100.01111110	.01111110.00111100.00011000