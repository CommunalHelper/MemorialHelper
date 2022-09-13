module MemorialHelperParallaxText

using ..Ahorn, Maple

@mapdef Entity "MemorialHelper/ParallaxText" ParallaxText(x::Integer, y::Integer, offsetX::Number=0.0, offsetY::Number=0.0, parallaxX::Number=1.75, parallaxY::Number=1.75, visibleDistanceX::Number=32.0, visibleDistanceY::Number=32.0, textScalarX::Number=1.25, textScalarY::Number=1.25, color::String="ffffff", borderColor::String="000000", dialog::String="", flag::String="", border::Bool=false)

function textFinalizer(entity)
    x, y = Ahorn.position(entity)

    width = Int(get(entity.data, "width", 8))
    height = Int(get(entity.data, "height", 8))

    entity.data["nodes"] = [(x + Int(width/2), y - 4)]
end

const placements = Ahorn.PlacementDict(
    "Parallax Text (Memorial Helper)" => Ahorn.EntityPlacement(
        ParallaxText,
		"rectangle",
		Dict{String, Any}(),
		textFinalizer
    )
)

fc = (1.0, 1.0, 1.0, 0.25)
bc = (1.0, 1.0, 1.0, 0.75)

Ahorn.minimumSize(entity::ParallaxText) = 8, 8
Ahorn.resizable(entity::ParallaxText) = true, true

Ahorn.nodeLimits(entity::ParallaxText) = 1, 1


function Ahorn.selection(entity::ParallaxText)
	
	if(haskey(entity.data, "nodes") && !isempty(entity.data["nodes"]))
		textX, textY = Int.(entity.data["nodes"][1])
		textX += get(entity.data, "offsetX", 0)
		textY += get(entity.data, "offsetY", 0)
		return [Ahorn.getEntityRectangle(entity), Ahorn.Rectangle(textX - 8, textY - 4, 16, 8)]
	else
		return Ahorn.getEntityRectangle(entity)
	end
	
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::ParallaxText, room::Maple.Room)
    width = Int(get(entity.data, "width", 8))
    height = Int(get(entity.data, "height", 8))
	Ahorn.drawRectangle(ctx, 1, 1, width - 2 , height - 2, fc, bc)
	Ahorn.drawCenteredText(ctx, "Parallax Text", 0, 0, width, height)
end

function Ahorn.renderSelectedAbs(ctx::Ahorn.Cairo.CairoContext, entity::ParallaxText, room::Maple.Room)
    
	x, y = Ahorn.position(entity)
	
	width = Int(get(entity.data, "width", 8))
	height = Int(get(entity.data, "height", 8))
	
	textX, textY = haskey(entity.data, "nodes") && !isempty(entity.data["nodes"]) ? Int.(entity.data["nodes"][1]) : (x + Int(width/2), y + Int(height/2))
	textX += get(entity.data, "offsetX", 0)
	textY += get(entity.data, "offsetY", 0)
	Ahorn.drawCenteredText(ctx, "Text", textX, textY, 0, 0)
	Ahorn.drawArrow(ctx, x + width / 2, y + height / 2, textX, textY, Ahorn.colors.selection_selected_fc, headLength=6)
	
end

end