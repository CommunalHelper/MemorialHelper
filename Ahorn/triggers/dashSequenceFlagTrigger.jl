module MemorialHelperDashComboFlagTrigger

using ..Ahorn, Maple

@mapdef Trigger "MemorialHelper/DashSequenceFlagTrigger" DashSequenceFlagTrigger(x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight, sequence::String="0 1 2 3 4", flag::String="", repeatable::Bool=false, persistent::Bool=false)

const placements = Ahorn.PlacementDict(
   "Dash Sequence Flag Trigger (Memorial Helper)" => Ahorn.EntityPlacement(
      DashSequenceFlagTrigger,
	  "rectangle"
   )
)

end