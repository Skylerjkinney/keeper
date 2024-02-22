import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { Keep } from "../models/Keep"

class ProfileService {
    async getProfileById(profileId) {
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log('Got Profile in service layer', response.data)
        const newProfile = new Profile(response.data)
        AppState.activeProfile = newProfile
    }
    async getProfileKeeps(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('getting profile keeps', response.data)
        let profileKeeps = response.data.map(keep => new Keep(keep))
        AppState.keeps = profileKeeps
    }
    async getProfileVaults(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('getting profile vaults', response.data)
        let profileVaults = response.data.map(vault => new Vault(vault))
        AppState.vaults = profileVaults
    }
}
export const profileService = new ProfileService()