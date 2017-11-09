function validateForm() {
    //We will require each field, so we need to access all user input

    var name = document.forms['main-contact-form']['name'].value;
    //First set of square brackets: string value of teh form ID or name
    //Second set of square brackets: string value of HTML control ID or name
    var email = document.forms['main-contact-form']['email'].value;
    var subject = document.forms['main-contact-form']['subject'].value;
    var message = document.forms['main-contact-form']['message'].value;

    //Access error message spans
    var rfvName = document.getElementById('rfvName');
    var rfvEmail = document.getElementById('rfvEmail');
    var rfvSubject = document.getElementById('rfvSubject');
    var rfvMessage = document.getElementById('rfvMessage')

    //Declare a regular expression object for email
    var emailRegEx = new RegExp(/^[A-Z0-9._-]+@[A-Z0-9.-]+\.[A-Z0-9.-]+$/i);
    //Use a regular expression to verify that the name enter only
    //includes alphabetical characters no numbers

    var nameRegEx = new RegExp(/[a-zA-Z\-'\s]+/);

    //Test the length of each field and regular expressions
    if (name.length == 0 || email.length == 0 || subject.length == 0
        || message.length == 0 || !emailRegEx.test(email) || !nameRegEx.test(name)) {
        //Error messages for required fields
        if (name.length == 0) {
            rfvName.textContent = "* Name is required.";
        }//end if
        else {
            rfvName.textContent = "";
        }
        if (email.length == 0) {
            rfvEmail.textContent = "* Email is required.";
        }//end if
        else {
            rfvEmail.textContent = "";
        }
        if (subject.length == 0) {
            rfvSubject.textContent = "* Subject is required.";
        }//end if
        else {
            rfvSubject.textContent = "";
        }
        if (message.length == 0) {
            rfvMessage.textContent = "* Message is required.";
        }//end if
        else {
            rfvMessage.textContent = "";
        }

        //Error message for RegEx
        if (!emailRegEx.test(email) && email.length > 0) {
            rfvEmail.textContent = "* Please enter a valid email.";
        }
        if (emailRegEx.test(email) && email.length > 0) {
            rfvEmail.textContent = "";
        }
        if (!nameRegEx.test(name) && name.length > 0) {
            rfvName.textContent = "* Please enter a name using only letters.";
        }
        if (nameRegEx.test(name) && name.length > 0) {
            rfvName.textContent = "";
        }
        //Stops form from sending
        event.preventDefault();

    }//end main if
}//end functions