import { AppState } from "../AppState"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"
import { logger } from "../utils/Logger"

class VaultsService {
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('vaulting vault', response.data)
        const newVault = new Vault(response.data)
        AppState.vaults.unshift(newVault)
        return newVault
    }
    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log('Vault, deleted', response.data)
        const indexToRemove = AppState.vaults.findIndex(vault => vault.id == vaultId)
        AppState.vaults.splice(indexToRemove, 1)
    }
    async getVaultById(vaultId) {
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${vaultId}`)
        logger.log("getting vault", response.data)
        AppState.activeVault = new Vault(response.data)
    }
}

export const vaultsService = new VaultsService()