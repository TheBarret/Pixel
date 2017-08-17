;************************************
; Program: Call/Stack Testing
;************************************

:i	0
:main
	call [s1]
	rsp
	jmp [main]

:stack
	inc [i] #1
	if [i] 0xff call [reset]
	load [i]
	return
:reset
	storev [i] #0
	return
:s1	call [stack]
	call [s2]
	return
:s2	call [stack]
	call [s3]
	return
:s3	call [stack]
	call [s4]
	return
:s4	call [stack]
	call [s5]
	return
:s5	call [stack]
	call [s6]
	return
:s6	call [stack]
	call [s7]
	return
:s7	call [stack]
	call [s8]
	return
:s8	call [stack]
	call [s9]
	return
:s9	call [stack]
	call [s10]
	return
:s10	call [stack]
	return

