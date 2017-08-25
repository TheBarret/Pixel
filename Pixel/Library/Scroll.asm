;*************************************************************
; NAME			: SCROLL
; DESCRIPTION	: SHIFT DISPLAY BUFFER
; PARAMS		: DIRECTION STEPS
;*************************************************************

:@scroll
	store [@scroll_steps]
	store [@scroll_direction]
	scroll [@scroll_direction][@scroll_steps]
	storev [@scroll_steps] #0
	storev [@scroll_direction] #0
	return
:@scroll_steps		0
:@scroll_direction	0
	