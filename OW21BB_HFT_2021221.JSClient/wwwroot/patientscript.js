let patients = [];
let connection = null;

let patientIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR(){
    connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:41147/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

    connection.on("PatientCreated",(user,message) =>{
        getdata();
    });

    connection.on("PatientDeleted",(user,message) =>{
        getdata();
    });

    connection.on("PatientUpdated",(user,message) =>{
        getdata();
    });

    connection.onclose(async () => { 
        await start();
    });
    start();
}

async function start() {
    try{
        await connection.start();
        console.log('SignalR Connected.');
    } catch(err){
        console.log(err);
        setTimeout(start,5000);
    }
}


async function getdata() {
    fetch('http://localhost:41147/patient')
        .then(x => x.json())
        .then(y => {
            patients = y;
            //console.log(hospitals);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    patients.forEach(patient => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + patient.patientID + "</td><td>"
            + patient.name + "</td><td>" 
            + patient.age + "</td><td>" 
            + patient.address + "</td><td>" 
            + patient.disease + "</td><td>" 
            + `<button type="button" onclick="remove(${patient.patientID})">Delete</button>`
            + `<button type="button" onclick="showupdate(${patient.patientID})">Update</button>`
            + "</td></tr>";
        //console.log(hospital.name);
    });
}

function showupdate(id){
    document.getElementById('patientnametoupdate').value = 
    patients.find(x=>x['patientID'] == id)['name'];

    document.getElementById('patientagetoupdate').value = 
    patients.find(x=>x['patientID'] == id)['age'];

    document.getElementById('patientaddresstoupdate').value = 
    patients.find(x=>x['patientID'] == id)['address'];

    document.getElementById('patientdiseasetoupdate').value = 
    patients.find(x=>x['patientID'] == id)['disease'];

    document.getElementById('updateformdiv').style.display = "flex";
   patientIdToUpdate = id;
}

function update() {
    let name = document.getElementById('patientnametoupdate').value;
    let age = document.getElementById('patientagetoupdate').value;
    let address = document.getElementById('patientaddresstoupdate').value;
    let disease = document.getElementById('patientdiseasetoupdate').value;

    document.getElementById('updateformdiv').style.display = 'none';

    fetch('http://localhost:41147/patient', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {patientID: patientIdToUpdate, name: name, age: age, address: address, disease: disease}
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}




function create() {
    let name = document.getElementById('patientname').value;
    let age = document.getElementById('patientspec').value;
    let address = document.getElementById('patientaddress').value;
    let disease = document.getElementById('patientdisease').value;

    fetch('http://localhost:41147/patient', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {name: name, age: age, address: address, disease: disease}
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id){
    fetch('http://localhost:41147/patient/' + id,{
        method: 'DELETE',
        headers: {'Content-Type': 'application/json',},
        body: null})
        .then(response => response)
        .then(data =>{
            console.log('Success',data);
            getdata();
        })
        .catch((error)=>{console.log('Error:',error);});    
}

