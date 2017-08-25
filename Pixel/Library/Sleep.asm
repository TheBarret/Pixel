;*************************************************************
; NAME			: SLEEP
; DESCRIPTION	: MAKE VM SLEEP FOR N CYCLES
; PARAMS		: N
;*************************************************************

:@sleep
	store [@sleep_timer]
:@sleep_loop
	if [@sleep_timer] #0 return
	dec [@sleep_timer] #1
	jmp [@sleep_loop]
:@sleep_timer	0
	