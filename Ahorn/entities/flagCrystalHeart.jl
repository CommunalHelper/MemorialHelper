module MemorialHelperFlagCrystalHeart

using ..Ahorn, Maple

@mapdef Entity "MemorialHelper/FlagCrystalHeart" FlagCrystalHeart(x::Integer, y::Integer, flag::String="", fake::Bool=false, removeCameraTriggers::Bool=false)

const placements = Ahorn.PlacementDict(
   "Flag Crystal Heart (Memorial Helper)" => Ahorn.EntityPlacement(
      FlagCrystalHeart
   )
)

function Ahorn.selection(entity::FlagCrystalHeart)
    x, y = Ahorn.position(entity)
    return Ahorn.getSpriteRectangle("collectables/heartGem/white00.png", x, y)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::FlagCrystalHeart, room::Maple.Room)
    Ahorn.drawSprite(ctx, "collectables/heartGem/white00.png", 0, 0)
end

end