import { AppState } from "../AppState"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {
    async getKeeps() {
        const response = await api.get('api/keeps')
        logger.log('getting keeps', response.data)
        let newKeeps = response.data.map(keep => new Keep(keep))
        AppState.keeps = newKeeps
    }
    setActiveKeep(keepId) {
        const activeKeep = AppState.keeps.find(keep => keep.id == keepId)
        AppState.activeKeep = activeKeep
    }
}

export const keepsService = new KeepsService()