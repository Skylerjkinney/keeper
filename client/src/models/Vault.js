export class Vault {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.creatorId = data.creatorId
        this.creator = data.creator
        this.isPrivate = data.isPrivate
    }
}