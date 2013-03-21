$(document).ready(function () {
    if (!$('#myCanvas').tagcanvas({
        textColour: '#656565',
        outlineColour: '#8B6C55',
        weight: true,
        outlineThickness: 1,
        zoom: 0.8,
        maxSpeed: 0.05,
        depth: 0.75,
        textHeight: 25
    }, 'tagcloud'));
});
