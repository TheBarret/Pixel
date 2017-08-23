;************************************
; Program: string compare test
;************************************
clear | jmp [main]

:x	10
:y	10
:i	0
:len	0

:stra		"abcde"
:strb		"abcde"

:true		"String is equal"

:main
	strcmp [stra][strb] call [print_true]
	end

:print_true
	strlen [true][len]
	print [x][y][i][true]
	inc [i] #1
	inc [x] #6
	ifv [i][len] return
	jmp [print_true]
