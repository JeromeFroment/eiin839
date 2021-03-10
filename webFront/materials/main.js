var stations;
var contracts;

function retrieveAllContracts() {
    url = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("id").value;

    req = new XMLHttpRequest();
    req.open("GET", url, true);

    req.setRequestHeader("Accept", "application/json");
    req.onload = contractRetrieved;

    req.send();
}

function contractRetrieved() {
    contracts = JSON.parse(this.responseText);
    contracts.forEach(r => {
        var option = document.createElement("option");
        option.innerHTML = r.name;
        document.getElementById("datalist").appendChild(option);
    })
}

function retrieveContractStations() {

    url = "https://api.jcdecaux.com/vls/v3/stations?contract=" + document.getElementById("input2").value + "&apiKey=" + document.getElementById("id").value;
    req = new XMLHttpRequest();
    req.open("GET", url, true);

    req.setRequestHeader("Accept", "application/json");
    req.onload = contractStationsRetrieved;

    req.send();
}

function contractStationsRetrieved() {
    stations = JSON.parse(this.responseText);
    console.log(stations);
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2-lat1);
    var dLon = deg2rad(lon2-lon1);
    var a =
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI/180)
}

function getClosestStation() {
    var lat = document.getElementById("input3").value;
    var long = document.getElementById("input4").value;

    var closest = stations[0].name;
    var distance = getDistanceFrom2GpsCoordinates(lat, long, stations[0].position.latitude, stations[0].position.longitude);

    for (let i = 0; i < stations.length; i++)
    {
        var distanceTemp = getDistanceFrom2GpsCoordinates(lat, long, stations[i].position.latitude, stations[i].position.longitude);
        if (distanceTemp <= distance)
        {
            distance = distanceTemp;
            closest = stations[i].name;
        }
    }
    console.log(closest);
    var response = document.createElement("div");
    response.innerHTML = "Station la plus proche => " + closest;
    document.body.append(response);
}