;************************************
; Program: Display counter
;************************************
clear | jmp [main]

:counter	0
:string	"Count:"
:x		10
:y		10
:i		0
:len		0
:timer		0

:main	call [print_text]			;print out text
	storev [x] #45			;increase x
:loop	if [counter] #100 call [reset]	;reset if 100
	call [print_counter]
	jmp [loop]

:print_counter
	printv [x][y][counter]		;Draw counter
	call [wait]				;Hang back..
	printv [x][y][counter]		;Reverse last draw
	inc [counter] #1			;increase counter
	return

:print_text
	print [x][y][i][string]
	inc [i] #1
	inc [x] #6
	strlen [string] [len]
	ifv [i] [len] return
	jmp [print_text]

:reset	storev [counter] #0
	return

:wait	storev [timer] #60
:wait_loop
	dec [timer] #1
	if [timer] #0 return
	jmp [wait_loop]