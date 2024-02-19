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
    async getKeepById(keepId) {
        AppState.activeKeep = null
        const response = await api.get(`api/keeps/${keepId}`)
        logger.log("getting keep", response.data)
        AppState.activeKeep = new Keep(response.data)
    }
}

export const keepsService = new KeepsService()