;************************************
; Program: Bouncing Ball
;************************************
clear | jmp [init]

:x	0
:y	0
:vx	0
:vy	0
:step	2

:init	random #64 |	store [x]
	random #32 |	store [y]
	random #1  |	store [vx]
	random #1  |	store [vy]
	
:main	if [vx] #0 call [left]
	if [vx] #1 call [right]
	if [vy] #0 call [up]
	if [vy] #1 call [down]
	call [draw]
	call [check]
	call [draw]
	jmp [main]

:draw	draw [x][y][ball] | return

:check	
	ifg [x] #120 call [change_left]
	iflv[x] [step] call [change_right]
	ifg [y] #58 call [change_up]
	iflv[y] [step] call [change_down]
	return

:left	load [x] | load [step] | sub | store [x] | return
:right	load [x] | load [step] | add | store [x] | return
:up		load [y] | load [step] | sub | store [y] | return
:down	load [y] | load [step] | add | store [y] | return

:change_left  | storev [vx] #0 | return
:change_right | storev [vx] #1 | return
:change_up    | storev [vy] #0 | return
:change_down  | storev [vy] #1 | return

:ball	.00011000
	.00110100
	.01111110
	.01111110
	.00111100
	.00011000