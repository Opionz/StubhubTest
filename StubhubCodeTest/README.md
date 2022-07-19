## 1.)
	1.) 
	2.) Question : How would you call the AddToEmail method in order to send the events in an email?
	    Answer: While iterating the list of events I will call the AddToEmail method in each iteration.
	3.) Question: What is the expected output if we only have the client John Smith?
		Answer: If John Smith isnt in the list it will give a null output.
	4.) Question: Do you believe there is a way to improve the code you first wrote?
		Answer: None

## 2.)
	1.) Question: What should be your approach to getting the distance between the customer’s city and
		the other cities on the list?
		Answer: Since the GetDistance method works further uses the AphabeticalDistance method, It is posible to size the different distances with numerical values so it ok to save the difference between distances with numerical computed values in ascending order.
	2.) Question: How would you get the 5 closest events and how would you send them to the client in an
		email?
		Answer: From the question above, since the Cached Distance and its corresponding value is saved in ascending order. Its easy to query the least 5 events based on distance and iterate through them and send the email withing each iteration count. 
	3.) Question: What is the expected output if we only have the client John Smith?
		Answer: If John Smith isnt in the list it will give a null output.
	4.) Question: Do you believe there is a way to improve the code you first wrote?
		Answer: Caching 