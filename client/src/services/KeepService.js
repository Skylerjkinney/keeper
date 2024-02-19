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
}

export const keepsService = new KeepsService()