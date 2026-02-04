window.scrollToElement = function(elementId) {
    console.log('scrollToElement wywołane dla:', elementId);
    const element = document.getElementById(elementId);
    if (element) {
        console.log('Element znaleziony, scrolluję...');
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    } else {
        console.log('Element NIE znaleziony!');
    }
}
