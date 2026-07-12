export function getUserId(user) {
  return user?.id ?? user?.userId ?? user?.UserId;
}

export function getUserName(user) {
  return user?.userName ?? user?.username ?? user?.UserName ?? user?.Username;
}

export function getUserEmail(user) {
  return user?.eMail ?? user?.email ?? user?.EMail ?? user?.Email;
}

export function normalizeUser(user, detail = null) {
  const merged = { ...(user ?? {}), ...(detail ?? {}) };
  const userId = getUserId(merged);

  return {
    ...merged,
    id: userId,
    userId,
    userName: getUserName(merged),
    eMail: getUserEmail(merged),
  };
}
