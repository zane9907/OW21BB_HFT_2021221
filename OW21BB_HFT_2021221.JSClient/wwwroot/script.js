let hospitals = [];
let connection = null;

let hospitalIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR(){
    connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:41147/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

    connection.on("HospitalCreated",(user,message) =>{
        getdata();
    });

    connection.on("HospitalDeleted",(user,message) =>{
        getdata();
    });

    connection.on("HospitalUpdated",(user,message) =>{
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
    fetch('http://localhost:41147/hospital')
        .then(x => x.json())
        .then(y => {
            hospitals = y;
            //console.log(hospitals);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    hospitals.forEach(hospital => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + hospital.hospitalID + "</td><td>"
            + hospital.name + "</td><td>" 
            + `<button type="button" onclick="remove(${hospital.hospitalID})">Delete</button>`
            + `<button type="button" onclick="showupdate(${hospital.hospitalID})">Update</button>`
            + "</td></tr>";
        //console.log(hospital.name);
    });
}

function showupdate(id){
    document.getElementById('hospitalnametoupdate').value = 
    hospitals.find(x=>x['hospitalID'] == id)['name'];
    document.getElementById('updateformdiv').style.display = "flex";
    hospitalIdToUpdate = id;
}

function update() {
    let name = document.getElementById('hospitalnametoupdate').value;
    document.getElementById('updateformdiv').style.display = 'none';

    fetch('http://localhost:41147/hospital', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {hospitalID: hospitalIdToUpdate, name: name, location: "asd" }
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
    let name = document.getElementById('hospitalname').value;

    fetch('http://localhost:41147/hospital', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, location: "asd" }
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
    fetch('http://localhost:41147/hospital/' + id,{
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

