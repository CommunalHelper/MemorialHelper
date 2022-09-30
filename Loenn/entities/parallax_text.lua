local parallaxText = {}

parallaxText.name = "MemorialHelper/ParallaxText"

parallaxText.fieldInformation = {
    color = {
        fieldType = "color"
    },
    borderColor = {
        fieldType = "color"
    },
    visibleDistanceX = {
        minimumValue = 0.0
    },
    visibleDistanceY = {
        minimumValue = 0.0
    }
}

parallaxText.placements = {
    name = "parallax_text",
    data = {
        width = 8,
        height = 8,
        offsetX = 0.0,
        offsetY = 0.0,
        parallaxX = 1.75,
        parallaxY = 1.75,
        visibleDistanceX = 32.0,
        visibleDistanceY = 32.0,
        textScalarX = 1.25,
        textScalarY = 1.25,
        color = "ffffff",
        borderColor = "000000",
        dialog = "",
        flag = "",
        border = false
    }
}

parallaxText.fillColor = {1.0, 1.0, 1.0, 0.25}
parallaxText.borderColor = {1.0, 1.0, 1.0, 0.75}

return parallaxText