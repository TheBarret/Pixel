;************************************
; Program: Shooter
;************************************
clear | mode #0 | jmp [init]

:x		1
:y		15
:key		0
:up		{W}
:down		{S}
:shoot		{ }
:buffer	0
:s_value	0
:s_x		50
:s_y		1
:b_status	0
:b_x		0
:b_y		0

:init
	call [score_draw]
:main
	input [key]
	ifv [key][up] call [pistol_up]
	ifv [key][down] call [pistol_down]
	ifv [key][shoot] call [bullet_init]
	call [pistol_draw]
	call [bullet_moving]
	jmp [main]

:pistol_up 
	if [y] #0 return 
	dec [y] #1 
	return

:pistol_down 
	if [y] #26 return
	inc [y] #1
	return

:pistol_draw 
	draw [x][y][pistol]
	draw [x][y][pistol]
	return

:score_draw
	printv [s_x][s_y][s_value]
	return

:bullet_init
	if [b_status] #1 jmp [bullet_moving]
	storev [b_status] #1
	load [y]
	store [b_y]
	call [score_draw]
	inc [s_value] #1
	call [score_draw]
	return

:bullet_moving
	if [b_status] #0 return
	inc [b_x] #1
	if [b_x] #60 jmp [bullet_done]
	draw [b_x][b_y][bullet]
	draw [b_x][b_y][bullet]
	return

:bullet_done
	storev [b_status] #0
	storev [b_x] #0
	return

:pistol.00000000.00011111.00111111.00011100.00011000.00111000
:bullet.00000000.00000000.00000010.00000000.00000000.00000000
