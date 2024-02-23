import { api } from "./AxiosService"
import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"


class VaultKeepService {
    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultkeeps', vaultKeepData)
        logger.log('Creating VaultKeep', response.data)
        if (AppState.activeVault?.id == vaultKeepData.vaultId) {
            AppState.vaultKeeps.push(new VaultKeep(response.data))
        }
    }
    async deleteVaultKeep(vaultKeepId) {
        const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log('Deleting vaultKeep!', response.data)
        const indexToRemove = AppState.keeps.findIndex(keep => keep.vaultKeepId == vaultKeepId)
        AppState.keeps.splice(indexToRemove, 1)
    }
}

export const vaultKeepService = new VaultKeepService()