;************************************
; Program: Hello, World!
;************************************
clear | jmp [main]

:var	"Hello, World!"
	

:main
	push #20
	push #20
	push [var]
	call [@print]
	end

