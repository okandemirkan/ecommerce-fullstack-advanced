export const ADDRESS_TYPES = {
  Home: "Ev",
  Job: "İş",
  Other: "Diğer",
};

export function getAddressId(address) {
  return address?.id ?? address?.addressId ?? address?.AddressId;
}
