;************************************
; Program: Fibonacci
;************************************

include print

clear | mode #1 | jmp [main]

:param		15
:result	0
:result_text	"Fibonacci:"
:main

	load [param]		;load parameter
	call [fibonacci]	;call function
	store [result]	;store result

	push #10		;Show param, text and result
	push #10
	push [param]
	call [@printv]

	push #10
	push #20
	push [result_text]
	call [@print]


	push #70
	push #20
	push [result]
	call [@printv]
	end

;************************************
; Function	: fibonacci
; params	: int
;************************************

:fibonacci
	call [fibonacci_init]
:fibonacci_for
	iflv [fibonacci_i][fibonacci_n] jmp [fibonacci_loop]
	call [fibonacci_cleanup]
	load [fibonacci_a]
	return
:fibonacci_loop
	load [fibonacci_a]
	store [fibonacci_t]
	load [fibonacci_b]
	store [fibonacci_a]
	load [fibonacci_t]
	load [fibonacci_b]
	add
	store [fibonacci_b]
	inc [fibonacci_i] #1
	jmp [fibonacci_for]
:fibonacci_init
	store [fibonacci_n]
	storev [fibonacci_a] #0
	storev [fibonacci_b] #1
	storev [fibonacci_i] #0
	return
:fibonacci_cleanup
	storev [fibonacci_b] #0
	storev [fibonacci_t] #0
	storev [fibonacci_n] #0
	storev [fibonacci_i] #0
	return
:fibonacci_a 		0
:fibonacci_b 		0
:fibonacci_t 		0
:fibonacci_n 		0
:fibonacci_i 		0
