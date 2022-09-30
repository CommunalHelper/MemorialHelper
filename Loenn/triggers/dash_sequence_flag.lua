local dashSequenceFlag = {}

dashSequenceFlag.name = "MemorialHelper/DashSequenceFlagTrigger"
dashSequenceFlag.placements = {
    name = "dash_sequence_flag",
    data = {
        sequence = "0 1 2 3 4",
        flag = "",
        repeatable = false,
        persistent = false
    }
}

return dashSequenceFlag