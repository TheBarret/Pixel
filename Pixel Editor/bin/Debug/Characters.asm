;************************************
; Program: Characters
;************************************

include print

clear | jmp [main]

:var1	"abcdef 1234 ?!,."
:var2	"ghijkl 5678 +-*/"
:var3	"mnopqr 90   [|]\"
:var4	"stuvwxyz    :'="
	
:main
	push #1 | push #10 | push [var1]
	push #1 | push #20 | push [var2]
	push #1 | push #30 | push [var3]
	push #1 | push #40 | push [var4]
	call [@print]
	call [@print]
	call [@print]
	call [@print]
	end

