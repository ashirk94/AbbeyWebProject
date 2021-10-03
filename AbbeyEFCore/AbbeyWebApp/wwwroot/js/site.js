//altered to replace state/customer with monk data

class MonkPage {

    constructor() {
        this.state = {
            monkId: "",
            monk: null,
        };

        // instance variables that the app needs but are not part of the "state" of the application
        this.server = "http://localhost:5000";
        this.url = this.server + "/api/Monks";

        // instance variables related to ui elements simplifies code in other places
        this.$form = document.querySelector('#monkForm');
        this.$monkId = document.querySelector('#monkId');
        this.$monkFirstName = document.querySelector('#firstName');
        this.$monkLastName = document.querySelector('#lastName');
        this.$monkAge = document.querySelector('#age');
        this.$monkRole = document.querySelector('#role');
        this.$findButton = document.querySelector('#findBtn');
        this.$addButton = document.querySelector('#addBtn');
        this.$deleteButton = document.querySelector('#deleteBtn');
        this.$editButton = document.querySelector('#editBtn');
        this.$saveButton = document.querySelector('#saveBtn');
        this.$cancelButton = document.querySelector('#cancelBtn');

        // call these methods to set up the page
        this.bindAllMethods();
        this.makeFieldsReadOnly(true);
        this.makeFieldsRequired(false);
        this.enableButtons("pageLoad");

    }

    // any method that is used as part of an event handler must bind this to the class
    // not all of these methods need to be bound but it was easier to do them all as I wrote them
    bindAllMethods() {
        this.onFindMonk = this.onFindMonk.bind(this);
        this.onEditMonk = this.onEditMonk.bind(this);
        this.onCancel = this.onCancel.bind(this);
        this.onDeleteMonk = this.onDeleteMonk.bind(this);
        this.onSaveMonk = this.onSaveMonk.bind(this);
        this.onAddMonk = this.onAddMonk.bind(this);

        this.makeFieldsReadOnly = this.makeFieldsReadOnly.bind(this);
        this.makeFieldsRequired = this.makeFieldsRequired.bind(this);
        this.fillMonkFields = this.fillMonkFields.bind(this);
        this.clearMonkFields = this.clearMonkFields.bind(this);
        this.disableButtons = this.disableButtons.bind(this);
        this.disableButton = this.disableButton.bind(this);
        this.enableButtons = this.enableButtons.bind(this);
    }

    // makes an api call to /api/monk/# where # is the primary key of the monk
    // finds a monk based on monk id.  in a future version it would be better to search by name
    onFindMonk(event) {
        event.preventDefault();
        if (this.$monkId.value != "") {
            this.state.monkId = this.$monkId.value;
            fetch(`${this.url}/${this.state.monkId}`)
                .then(response => response.json())
                .then(data => {
                    if (data.status == 404) {
                        alert('That monk does not exist in our database');
                    }
                    else {
                        this.state.monk = data;
                        this.fillMonkFields();
                        this.enableButtons("found");
                    }
                })
                .catch(error => {
                    alert('There was a problem getting monk info!');
                    console.log(error);
                });
        }
        else {
            this.clearMonkFields();
        }
    }

    // makes a delete request to /api/monk/# where # is the primary key of the monk
    // deletes the monk displayed on the screen from the database
    onDeleteMonk(event) {
        event.preventDefault();
        if (this.state.monkId != "") {
            fetch(`${this.url}/${this.state.monkId}`, { method: 'DELETE' })
                .then(response => response.json())
                .then(data => {
                    // returns the record that we deleted so the ids should be the same 
                    if (this.state.monkId == data.monkId) {
                        this.state.monkId = "";
                        this.state.monk = null;
                        this.$monkId.value = "";
                        this.clearMonkFields();
                        this.enableButtons("pageLoad");
                        alert("Monk was deleted.")
                    }
                    else {
                        alert('There was a problem deleting monk info!');
                    }
                })
                .catch(error => {
                    alert('There was a problem deleting monk info!');
                });
        }
        else {
            // this should never happen if the right buttons are enabled
        }
    }

    // makes either a post or a put request to /api/monks
    // either adds a new monk or updates an existing monk in the database
    // based on the monk information in the form
    onSaveMonk(event) {
        event.preventDefault();
        // adding
        if (this.state.monkId == "") {
            fetch(`${this.url}`, {
                method: 'POST',
                body: JSON.stringify({
                    monkId: 0,
                    firstName: this.$monkFirstName.value,
                    lastName: this.$monkLastName.value,
                    age: parseInt(this.$monkAge.value),
                    job: this.$monkRole.value
                }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    // returns the record that we added so the ids should be there 
                    if (data.monkId) {
                        this.state.monkId = data.monkId;
                        this.state.monk = data;
                        this.$monkId.value = this.state.monkId;
                        this.fillMonkFields();
                        this.$monkId.readOnly = false;
                        this.enableButtons("found");
                        alert("Monk was added.")
                    }
                    else {
                        alert('There was a problem adding monk info!');
                    }
                })
                .catch(error => {
                    alert('There was a problem adding monk info!');
                });
        }
        // updating
        else {
            // the format of the body has to match the original object exactly 
            // so make a copy of it and copy the values from the form
            let monk = Object.assign(this.state.monk);
            monk.firstName = this.$monkFirstName.value;
            monk.lastName = this.$monkLastName.value;
            monk.age = parseInt(this.$monkAge.value);
            monk.job = this.$monkRole.value;
            fetch(`${this.url}/${this.state.monkId}`, {
                method: 'PUT',
                body: JSON.stringify(monk),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    // doesn't return a body just a status code of 204 
                    if (response.status == 204) {
                        this.state.monk = Object.assign(monk);
                        this.fillMonkFields();
                        this.$monkId.readOnly = false;
                        this.enableButtons("found");
                        alert("Monk was updated.")
                    }
                    else {
                        alert('There was a problem updating monk info!');
                    }
                })
                .catch(error => {
                    alert('There was a problem adding monk info!');
                });
        }
    }

    // makes the fields editable
    onEditMonk(event) {
        event.preventDefault();
        // can't edit the monk id
        this.$monkId.readOnly = true;
        this.makeFieldsReadOnly(false);
        this.makeFieldsRequired(true);
        this.enableButtons("editing");
    }

    // clears the form for input of a new monk
    onAddMonk(event) {
        event.preventDefault();
        // can't edit the monk id
        this.state.monkId = ""
        this.state.monk = null;
        this.$monkId.value = "";
        this.$monkId.readOnly = true;
        this.clearMonkFields();
        this.makeFieldsReadOnly(false);
        this.makeFieldsRequired(true);
        this.enableButtons("editing");
    }

    // cancels the editing for either a new monk or an existing monk
    onCancel(event) {
        event.preventDefault();
        if (this.state.monkId == "") {
            this.clearMonkFields();
            this.makeFieldsReadOnly();
            this.makeFieldsRequired(false);
            this.$monkId.readOnly = false;
            this.enableButtons("pageLoad");
        }
        else {
            this.fillMonkFields();
            this.$monkId.readOnly = false;
            this.enableButtons("found");
        }
    }

    // fills the form with data based on the monk
    fillMonkFields() {
        // fill the fields
        this.$monkFirstName.value = this.state.monk.firstName;
        this.$monkLastName.value = this.state.monk.lastName;
        this.$monkAge.value = this.state.monk.age;
        this.$monkRole.value = this.state.monk.job;
        this.makeFieldsReadOnly();
    }

    // clears the ui
    clearMonkFields() {
        this.$monkFirstName.value = "";
        this.$monkLastName.value = "";
        this.$monkAge.value = "";
        this.$monkRole.value = "";
    }

    // enables or disables ui elements
    makeFieldsReadOnly(readOnly = true) {
        this.$monkFirstName.readOnly = readOnly;
        this.$monkLastName.readOnly = readOnly;
        this.$monkAge.readOnly = readOnly;
        this.$monkRole.readOnly = readOnly;
    }

    // makes ui elements required when editing
    makeFieldsRequired(required = true) {
        this.$monkFirstName.required = required;
        this.$monkLastName.required = required;
        this.$monkAge.required = required;
        this.$monkRole.required = required;
    }

    // disables an array of buttons
    disableButtons(buttons) {
        buttons.forEach(b => b.onclick = this.disableButton);
        buttons.forEach(b => b.classList.add("disabled"));
    }

    // disables one button
    disableButton(event) {
        event.preventDefault();
    }

    // enables ui elements based on the editing state of the page
    enableButtons(state) {
        switch (state) {
            case "pageLoad":
                this.disableButtons([this.$deleteButton, this.$editButton, this.$saveButton, this.$cancelButton]);
                this.$findButton.onclick = this.onFindMonk;
                this.$findButton.classList.remove("disabled");
                this.$addButton.onclick = this.onAddMonk;
                this.$addButton.classList.remove("disabled");
                break;
            case "editing": case "adding":
                this.disableButtons([this.$deleteButton, this.$editButton, this.$addButton]);
                this.$saveButton.onclick = this.onSaveMonk;
                this.$cancelButton.onclick = this.onCancel;
                [this.$saveButton, this.$cancelButton].forEach(b => b.classList.remove("disabled"));
                break;
            case "found":
                this.disableButtons([this.$saveButton, this.$cancelButton]);
                this.$findButton.onclick = this.onFindMonk;
                this.$editButton.onclick = this.onEditMonk;
                this.$deleteButton.onclick = this.onDeleteMonk;
                this.$addButton.onclick = this.onAddMonk;
                [this.$findButton, this.$editButton, this.$deleteButton, this.$addButton].forEach(b => b.classList.remove("disabled"));
                break;
            default:
        }
    }

}

// instantiate the js app when the html page has finished loading
window.addEventListener("load", () => new MonkPage());
