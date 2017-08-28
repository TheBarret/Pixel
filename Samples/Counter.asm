;************************************
; Program: Display counter
;************************************
include print
include common

clear | mode #0 | jmp [main]

:counter	0
:string	"Count:"

:main	push #10
	push #10
	push [string]
	call [@print]
:loop	call [draw]
	push #60
	call [@sleep]
	call [draw]
	inc [counter] #1
	ifg [counter] #50 call [reset]
	jmp [loop]

:draw	push #45
	push #10
	push [counter]
	call [@printv]
	return

:reset	storev [counter] #0
	return

