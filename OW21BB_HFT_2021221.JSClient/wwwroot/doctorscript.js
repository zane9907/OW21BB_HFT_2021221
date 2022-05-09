let doctors = [];
let connection = null;

let doctorIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR(){
    connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:41147/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

    connection.on("DoctorCreated",(user,message) =>{
        getdata();
    });

    connection.on("DoctorDeleted",(user,message) =>{
        getdata();
    });

    connection.on("DoctorUpdated",(user,message) =>{
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
    fetch('http://localhost:41147/doctor')
        .then(x => x.json())
        .then(y => {
            doctors = y;
            //console.log(hospitals);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    doctors.forEach(doctor => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + doctor.doctorID + "</td><td>"
            + doctor.name + "</td><td>" 
            + doctor.specialization + "</td><td>" 
            + `<button type="button" onclick="remove(${doctor.doctorID})">Delete</button>`
            + `<button type="button" onclick="showupdate(${doctor.doctorID})">Update</button>`
            + "</td></tr>";
        //console.log(hospital.name);
    });
}

function showupdate(id){
    document.getElementById('doctornametoupdate').value = 
    doctors.find(x=>x['doctorID'] == id)['name'];

    document.getElementById('doctorspectoupdate').value = 
    doctors.find(x=>x['doctorID'] == id)['specialization'];

    document.getElementById('updateformdiv').style.display = "flex";
   doctorIdToUpdate = id;
}

function update() {
    let name = document.getElementById('doctornametoupdate').value;
    let spec = document.getElementById('doctorspectoupdate').value;

    document.getElementById('updateformdiv').style.display = 'none';

    fetch('http://localhost:41147/doctor', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {doctorID: doctorIdToUpdate, name: name, specialization: spec }
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
    let name = document.getElementById('doctorname').value;
    let spec = document.getElementById('doctorspec').value;

    fetch('http://localhost:41147/doctor', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, specialization: spec }
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
    fetch('http://localhost:41147/doctor/' + id,{
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

