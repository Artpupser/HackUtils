navigator.geolocation.getCurrentPosition(position => {
    const { latitude, longitude } = position.coords
    console.log(position)
    console.log(latitude)
    console.log(longitude)
})